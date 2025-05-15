
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
    public class MateriaService : IMateriaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MateriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MateriaDTO>> GetAllAsync()
        {
            var materias = await _context.Materias
                .AsNoTracking()
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<MateriaDTO>>(materias);
        }

        public async Task<MateriaDTO> GetByIdAsync(int id)
        {
            var materia = await _context.Materias
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (materia == null)
                return null;
            
            return _mapper.Map<MateriaDTO>(materia);
        }

        public async Task<MateriaDTO> CreateAsync(MateriaCreateDTO materiaDTO)
        {
            var materia = _mapper.Map<Materia>(materiaDTO);
            
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<MateriaDTO>(materia);
        }

        public async Task<MateriaDTO> UpdateAsync(int id, MateriaUpdateDTO materiaDTO)
        {
            var materia = await _context.Materias.FindAsync(id);
            
            if (materia == null)
                return null;
            
            _mapper.Map(materiaDTO, materia);
            
            _context.Materias.Update(materia);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<MateriaDTO>(materia);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            
            if (materia == null)
                return false;
            
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<MateriaDTO>> GetByFuncionarioAsync(int funcionarioId)
        {
            var materias = await _context.Materias
                .AsNoTracking()
                .Where(m => m.FuncionarioId == funcionarioId)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<MateriaDTO>>(materias);
        }
    }
}
