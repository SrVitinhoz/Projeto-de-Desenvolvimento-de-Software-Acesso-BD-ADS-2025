
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestaoEscolar.Models;
using GestaoEscolar.DTOs;
using GestaoEscolar.DBContext;
using Microsoft.EntityFrameworkCore;

namespace GestaoEscolar.Services
{
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
                .AsNoTracking()
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        }

        public async Task<AlunoDTO> GetByIdAsync(int id)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            
            if (aluno == null)
                return null;
            
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<AlunoDTO> CreateAsync(AlunoCreateDTO alunoDTO)
        {
            var aluno = _mapper.Map<Aluno>(alunoDTO);
            
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<AlunoDTO> UpdateAsync(int id, AlunoUpdateDTO alunoDTO)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            
            if (aluno == null)
                return null;
            
            _mapper.Map(alunoDTO, aluno);
            
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            
            if (aluno == null)
                return false;
            
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<AlunoDTO> GetByMatriculaAsync(string matricula)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Matriculas)
                .FirstOrDefaultAsync(a => a.Matriculas.Any(m => m.Id.ToString() == matricula));
            
            if (aluno == null)
                return null;
            
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<IEnumerable<AlunoDTO>> GetByTurmaAsync(int turmaId)
        {
            var alunos = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Matriculas)
                .Where(a => a.Matriculas.Any(m => m.TurmaId == turmaId))
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        }
    }
}
