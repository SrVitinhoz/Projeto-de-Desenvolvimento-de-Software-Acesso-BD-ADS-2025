using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gestao_Escolar.DbContext;

namespace Gestao_Escolar.Services
{
    public interface IMatriculaService : IBaseService<Matricula, MatriculaDTO, MatriculaCreateDTO, MatriculaUpdateDTO>
    {
        Task<IEnumerable<MatriculaDTO>> GetByAlunoIdAsync(int alunoId);
        Task<MatriculaDTO?> AtualizarStatusMatriculaAsync(int alunoId, string status, string descricao, int? funcionarioId);
    }

    public class MatriculaService : IMatriculaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MatriculaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatriculaDTO>> GetAllAsync()
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Funcionario)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MatriculaDTO>>(matriculas);
        }

        public async Task<MatriculaDTO?> GetByIdAsync(int id)
        {
            var matricula = await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<MatriculaDTO>(matricula);
        }

        public async Task<MatriculaDTO> CreateAsync(MatriculaCreateDTO createDto)
        {
            var matricula = _mapper.Map<Matricula>(createDto);
            matricula.Data = DateTime.Now;
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return _mapper.Map<MatriculaDTO>(matricula);
        }

        public async Task<MatriculaDTO?> UpdateAsync(int id, MatriculaUpdateDTO updateDto)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null) return null;

            _mapper.Map(updateDto, matricula);
            await _context.SaveChangesAsync();
            return _mapper.Map<MatriculaDTO>(matricula);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null) return false;

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MatriculaDTO>> FindAsync(Expression<Func<Matricula, bool>> predicate)
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Funcionario)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MatriculaDTO>>(matriculas);
        }

        public async Task<IEnumerable<MatriculaDTO>> GetByAlunoIdAsync(int alunoId)
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Funcionario)
                .Where(m => m.AlunoId == alunoId)
                .OrderByDescending(m => m.Data)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MatriculaDTO>>(matriculas);
        }

        public async Task<MatriculaDTO?> AtualizarStatusMatriculaAsync(int alunoId, string status, string descricao, int? funcionarioId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var aluno = await _context.Alunos.FindAsync(alunoId);
                if (aluno == null)
                {
                    throw new InvalidOperationException("Aluno não encontrado.");
                }

                // Atualizar status do aluno
                aluno.StatusMatricula = Enum.Parse<StatusMatricula>(status, true);

                // Registrar no histórico de matrículas
                var matricula = new Matricula
                {
                    AlunoId = alunoId,
                    Data = DateTime.Now,
                    Status = Enum.Parse<StatusMatricula>(status, true),
                    Descricao = descricao,
                    FuncionarioId = funcionarioId
                };

                _context.Matriculas.Add(matricula);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _mapper.Map<MatriculaDTO>(matricula);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}