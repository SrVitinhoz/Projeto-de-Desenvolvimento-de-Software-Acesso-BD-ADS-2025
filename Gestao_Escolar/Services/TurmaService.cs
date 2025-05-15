
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
    public class TurmaService : ITurmaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TurmaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TurmaDTO>> GetAllAsync()
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        }

        public async Task<TurmaDTO> GetByIdAsync(int id)
        {
            var turma = await _context.Turmas
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if (turma == null)
                return null;
            
            return _mapper.Map<TurmaDTO>(turma);
        }

        public async Task<TurmaDTO> CreateAsync(TurmaCreateDTO turmaDTO)
        {
            var turma = _mapper.Map<Turma>(turmaDTO);
            
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<TurmaDTO>(turma);
        }

        public async Task<TurmaDTO> UpdateAsync(int id, TurmaUpdateDTO turmaDTO)
        {
            var turma = await _context.Turmas.FindAsync(id);
            
            if (turma == null)
                return null;
            
            _mapper.Map(turmaDTO, turma);
            
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<TurmaDTO>(turma);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            
            if (turma == null)
                return false;
            
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<TurmaDTO>> GetByStatusAsync(string status)
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .Where(t => t.Status == status)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        }

        public async Task<IEnumerable<TurmaDTO>> GetByPeriodoAsync(string periodo)
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .Where(t => t.Periodo == periodo)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        }
    }
}
