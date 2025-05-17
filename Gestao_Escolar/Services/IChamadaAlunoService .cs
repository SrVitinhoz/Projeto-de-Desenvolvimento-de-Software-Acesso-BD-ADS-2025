using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gestao_Escolar.DbContext;

namespace Gestao_Escolar.Services
{
    public interface IChamadaAlunoService : IBaseService<ChamadaAluno, ChamadaAlunoDTO, ChamadaAlunoCreateDTO, ChamadaAlunoUpdateDTO>
    {
        Task<IEnumerable<ChamadaAlunoDTO>> GetByChamadaIdAsync(int chamadaId);
        Task<IEnumerable<ChamadaAlunoDTO>> GetByAlunoIdAsync(int alunoId);
    }

    public class ChamadaAlunoService : IChamadaAlunoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ChamadaAlunoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChamadaAlunoDTO>> GetAllAsync()
        {
            var chamadasAluno = await _context.ChamadasAluno
                .Include(c => c.Aluno)
                .Include(c => c.Chamada)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChamadaAlunoDTO>>(chamadasAluno);
        }

        public async Task<ChamadaAlunoDTO?> GetByIdAsync(int id)
        {
            var chamadaAluno = await _context.ChamadasAluno
                .Include(c => c.Aluno)
                .Include(c => c.Chamada)
                .FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<ChamadaAlunoDTO>(chamadaAluno);
        }

        public async Task<ChamadaAlunoDTO> CreateAsync(ChamadaAlunoCreateDTO createDto)
        {
            var chamadaAluno = _mapper.Map<ChamadaAluno>(createDto);
            _context.ChamadasAluno.Add(chamadaAluno);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChamadaAlunoDTO>(chamadaAluno);
        }

        public async Task<ChamadaAlunoDTO?> UpdateAsync(int id, ChamadaAlunoUpdateDTO updateDto)
        {
            var chamadaAluno = await _context.ChamadasAluno.FindAsync(id);
            if (chamadaAluno == null) return null;

            _mapper.Map(updateDto, chamadaAluno);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChamadaAlunoDTO>(chamadaAluno);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var chamadaAluno = await _context.ChamadasAluno.FindAsync(id);
            if (chamadaAluno == null) return false;

            _context.ChamadasAluno.Remove(chamadaAluno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ChamadaAlunoDTO>> FindAsync(Expression<Func<ChamadaAluno, bool>> predicate)
        {
            var chamadasAluno = await _context.ChamadasAluno
                .Include(c => c.Aluno)
                .Include(c => c.Chamada)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChamadaAlunoDTO>>(chamadasAluno);
        }

        public async Task<IEnumerable<ChamadaAlunoDTO>> GetByChamadaIdAsync(int chamadaId)
        {
            var chamadasAluno = await _context.ChamadasAluno
                .Include(c => c.Aluno)
                .Where(c => c.ChamadaId == chamadaId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChamadaAlunoDTO>>(chamadasAluno);
        }

        public async Task<IEnumerable<ChamadaAlunoDTO>> GetByAlunoIdAsync(int alunoId)
        {
            var chamadasAluno = await _context.ChamadasAluno
                .Include(c => c.Chamada)
                .Where(c => c.AlunoId == alunoId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChamadaAlunoDTO>>(chamadasAluno);
        }
    }
}