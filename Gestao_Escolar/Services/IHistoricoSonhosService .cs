using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gestao_Escolar.DbContext;

namespace Gestao_Escolar.Services
{
    public interface IHistoricoSonhosService : IBaseService<HistoricoSonhos, HistoricoSonhosDTO, HistoricoSonhosCreateDTO, HistoricoSonhosUpdateDTO>
    {
        Task<IEnumerable<HistoricoSonhosDTO>> GetByAlunoIdAsync(int alunoId);
        Task<HistoricoSonhosDTO> AdicionarSonhosAsync(int alunoId, int valor, string motivo, int? FuncionarioId);
        Task<HistoricoSonhosDTO?> SubtrairSonhosAsync(int alunoId, int valor, string motivo, int? FuncionarioId);
    }

    public class HistoricoSonhosService : IHistoricoSonhosService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HistoricoSonhosService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HistoricoSonhosDTO>> GetAllAsync()
        {
            var historicos = await _context.HistoricoSonhos
                .Include(h => h.Aluno)
                .Include(h => h.Funcionario)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HistoricoSonhosDTO>>(historicos);
        }

        public async Task<HistoricoSonhosDTO?> GetByIdAsync(int id)
        {
            var historico = await _context.HistoricoSonhos
                .Include(h => h.Aluno)
                .Include(h => h.Funcionario)
                .FirstOrDefaultAsync(h => h.Id == id);
            return _mapper.Map<HistoricoSonhosDTO>(historico);
        }

        public async Task<HistoricoSonhosDTO> CreateAsync(HistoricoSonhosCreateDTO createDto)
        {
            var historico = _mapper.Map<HistoricoSonhos>(createDto);
            historico.Data = DateTime.Now;
            _context.HistoricoSonhos.Add(historico);
            await _context.SaveChangesAsync();
            return _mapper.Map<HistoricoSonhosDTO>(historico);
        }

        public async Task<HistoricoSonhosDTO?> UpdateAsync(int id, HistoricoSonhosUpdateDTO updateDto)
        {
            var historico = await _context.HistoricoSonhos.FindAsync(id);
            if (historico == null) return null;

            _mapper.Map(updateDto, historico);
            await _context.SaveChangesAsync();
            return _mapper.Map<HistoricoSonhosDTO>(historico);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var historico = await _context.HistoricoSonhos.FindAsync(id);
            if (historico == null) return false;

            _context.HistoricoSonhos.Remove(historico);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<HistoricoSonhosDTO>> FindAsync(Expression<Func<HistoricoSonhos, bool>> predicate)
        {
            var historicos = await _context.HistoricoSonhos
                .Include(h => h.Aluno)
                .Include(h => h.Funcionario)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HistoricoSonhosDTO>>(historicos);
        }

        public async Task<IEnumerable<HistoricoSonhosDTO>> GetByAlunoIdAsync(int alunoId)
        {
            var historicos = await _context.HistoricoSonhos
                .Include(h => h.Funcionario)
                .Where(h => h.AlunoId == alunoId)
                .OrderByDescending(h => h.Data)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HistoricoSonhosDTO>>(historicos);
        }

        public async Task<HistoricoSonhosDTO> AdicionarSonhosAsync(int alunoId, int valor, string motivo, int? FuncionarioId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var aluno = await _context.Alunos.FindAsync(alunoId);
                if (aluno == null)
                {
                    throw new InvalidOperationException("Aluno não encontrado.");
                }

                // Adicionar ao saldo
                aluno.SaldoSonhos += valor;

                // Registrar no histórico
                var historico = new HistoricoSonhos
                {
                    AlunoId = alunoId,
                    Data = DateTime.Now,
                    Tipo = TipoOperacaoSonhos.Adição,
                    Motivo = motivo,
                    Valor = valor,
                    FuncionarioId = FuncionarioId
                };

                _context.HistoricoSonhos.Add(historico);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<HistoricoSonhosDTO>(historico);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<HistoricoSonhosDTO?> SubtrairSonhosAsync(int alunoId, int valor, string motivo, int? FuncionarioId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var aluno = await _context.Alunos.FindAsync(alunoId);
                if (aluno == null)
                {
                    throw new InvalidOperationException("Aluno não encontrado.");
                }

                // Verificar se tem saldo suficiente
                if (aluno.SaldoSonhos < valor)
                {
                    return null; // Saldo insuficiente
                }

                // Subtrair do saldo
                aluno.SaldoSonhos -= valor;

                // Registrar no histórico
                var historico = new HistoricoSonhos
                {
                    AlunoId = alunoId,
                    Data = DateTime.Now,
                    Tipo = TipoOperacaoSonhos.Subtração,
                    Motivo = motivo,
                    Valor = valor,
                    FuncionarioId = FuncionarioId
                };

                _context.HistoricoSonhos.Add(historico);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<HistoricoSonhosDTO>(historico);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}