using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gestao_Escolar.DbContext;

namespace Gestao_Escolar.Services
{
    public interface IMateriaService : IBaseService<Materia, MateriaDTO, MateriaCreateDTO, MateriaUpdateDTO>
    {
        Task<IEnumerable<MateriaDTO>> GetMateriasAtivasAsync();
    }

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
            var materias = await _context.Materias.ToListAsync();
            return _mapper.Map<IEnumerable<MateriaDTO>>(materias);
        }

        public async Task<MateriaDTO?> GetByIdAsync(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            return _mapper.Map<MateriaDTO>(materia);
        }

        public async Task<MateriaDTO> CreateAsync(MateriaCreateDTO createDto)
        {
            var materia = _mapper.Map<Materia>(createDto);
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            return _mapper.Map<MateriaDTO>(materia);
        }

        public async Task<MateriaDTO?> UpdateAsync(int id, MateriaUpdateDTO updateDto)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null) return null;

            _mapper.Map(updateDto, materia);
            await _context.SaveChangesAsync();
            return _mapper.Map<MateriaDTO>(materia);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null) return false;

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MateriaDTO>> FindAsync(Expression<Func<Materia, bool>> predicate)
        {
            var materias = await _context.Materias.Where(predicate).ToListAsync();
            return _mapper.Map<IEnumerable<MateriaDTO>>(materias);
        }

        public async Task<IEnumerable<MateriaDTO>> GetMateriasAtivasAsync()
        {
            var materias = await _context.Materias
                .Where(m => m.Status == StatusMateria.Ativo)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MateriaDTO>>(materias);
        }
    }
}