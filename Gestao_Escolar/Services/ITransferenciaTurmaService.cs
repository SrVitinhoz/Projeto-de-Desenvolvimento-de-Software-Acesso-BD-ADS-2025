using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gestao_Escolar.DbContext;

namespace Gestao_Escolar.Services
{
    public interface ITransferenciaTurmaService : IBaseService<TransferenciaTurma, TransferenciaTurmaDTO, TransferenciaTurmaCreateDTO, TransferenciaTurmaUpdateDTO>
    {
        Task<IEnumerable<TransferenciaTurmaDTO>> GetByAlunoIdAsync(int alunoId);
        Task<TransferenciaTurmaDTO?> RealizarTransferenciaAsync(TransferenciaTurmaCreateDTO transferencia);
    }

    public class TransferenciaTurmaService : ITransferenciaTurmaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TransferenciaTurmaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransferenciaTurmaDTO>> GetAllAsync()
        {
            var transferencias = await _context.TransferenciasTurma
                .Include(t => t.Aluno)
                .Include(t => t.TurmaOrigem)
                .Include(t => t.TurmaDestino)
                .Include(t => t.Funcionario)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TransferenciaTurmaDTO>>(transferencias);
        }

        public async Task<TransferenciaTurmaDTO?> GetByIdAsync(int id)
        {
            var transferencia = await _context.TransferenciasTurma
                .Include(t => t.Aluno)
                .Include(t => t.TurmaOrigem)
                .Include(t => t.TurmaDestino)
                .Include(t => t.Funcionario)
                .FirstOrDefaultAsync(t => t.Id == id);
            return _mapper.Map<TransferenciaTurmaDTO>(transferencia);
        }

        public async Task<TransferenciaTurmaDTO> CreateAsync(TransferenciaTurmaCreateDTO createDto)
        {
            var transferencia = _mapper.Map<TransferenciaTurma>(createDto);
            _context.TransferenciasTurma.Add(transferencia);
            await _context.SaveChangesAsync();
            return _mapper.Map<TransferenciaTurmaDTO>(transferencia);
        }

        public async Task<TransferenciaTurmaDTO?> UpdateAsync(int id, TransferenciaTurmaUpdateDTO updateDto)
        {
            var transferencia = await _context.TransferenciasTurma.FindAsync(id);
            if (transferencia == null) return null;

            _mapper.Map(updateDto, transferencia);
            await _context.SaveChangesAsync();
            return _mapper.Map<TransferenciaTurmaDTO>(transferencia);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transferencia = await _context.TransferenciasTurma.FindAsync(id);
            if (transferencia == null) return false;

            _context.TransferenciasTurma.Remove(transferencia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TransferenciaTurmaDTO>> FindAsync(Expression<Func<TransferenciaTurma, bool>> predicate)
        {
            var transferencias = await _context.TransferenciasTurma
                .Include(t => t.Aluno)
                .Include(t => t.TurmaOrigem)
                .Include(t => t.TurmaDestino)
                .Include(t => t.Funcionario)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TransferenciaTurmaDTO>>(transferencias);
        }

        public async Task<IEnumerable<TransferenciaTurmaDTO>> GetByAlunoIdAsync(int alunoId)
        {
            var transferencias = await _context.TransferenciasTurma
                .Include(t => t.TurmaOrigem)
                .Include(t => t.TurmaDestino)
                .Include(t => t.Funcionario)
                .Where(t => t.AlunoId == alunoId)
                .OrderByDescending(t => t.DataTransferencia)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TransferenciaTurmaDTO>>(transferencias);
        }

        public async Task<TransferenciaTurmaDTO?> RealizarTransferenciaAsync(TransferenciaTurmaCreateDTO transferencia)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var aluno = await _context.Alunos.FindAsync(transferencia.AlunoId);
                if (aluno == null)
                {
                    throw new InvalidOperationException("Aluno não encontrado.");
                }

                // Registrar a transferência
                var novaTransferencia = new TransferenciaTurma
                {
                    AlunoId = transferencia.AlunoId,
                    TurmaOrigemId = aluno.TurmaId,
                    TurmaDestinoId = transferencia.TurmaDestinoId,
                    DataTransferencia = transferencia.DataTransferencia,
                    FuncionarioId = transferencia.FuncionarioId
                };

                _context.TransferenciasTurma.Add(novaTransferencia);

                // Atualizar a turma do aluno
                aluno.TurmaId = transferencia.TurmaDestinoId;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<TransferenciaTurmaDTO>(novaTransferencia);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}