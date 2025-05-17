using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestao_Escolar.DbContext;
using System.Linq.Expressions;

namespace Gestao_Escolar.Services
{
    public interface IAlunoService : IBaseService<Aluno, AlunoDTO, AlunoCreateDTO, AlunoUpdateDTO>
    {
        Task<bool> AtualizarSaldoSonhosAsync(int alunoId, int valor);
        Task<IEnumerable<AlunoDTO>> GetAlunosByTurmaAsync(int turmaId);
    }

    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AlunoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlunoDTO>> GetAllAsync()
        {
            var alunos = await _context.Alunos
                .Include(a => a.Turma)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        }

        public async Task<AlunoDTO?> GetByIdAsync(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<AlunoDTO> CreateAsync(AlunoCreateDTO createDto)
        {
            var aluno = _mapper.Map<Aluno>(createDto);
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<AlunoDTO?> UpdateAsync(int id, AlunoUpdateDTO updateDto)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return null;

            _mapper.Map(updateDto, aluno);
            await _context.SaveChangesAsync();
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return false;

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AlunoDTO>> FindAsync(Expression<Func<Aluno, bool>> predicate)
        {
            var alunos = await _context.Alunos
                .Include(a => a.Turma)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        }

        public async Task<bool> AtualizarSaldoSonhosAsync(int alunoId, int valor)
        {
            var aluno = await _context.Alunos.FindAsync(alunoId);
            if (aluno == null) return false;

            aluno.SaldoSonhos += valor;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AlunoDTO>> GetAlunosByTurmaAsync(int turmaId)
        {
            var alunos = await _context.Alunos
                .Include(a => a.Turma)
                .Where(a => a.TurmaId == turmaId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        }
    }
}