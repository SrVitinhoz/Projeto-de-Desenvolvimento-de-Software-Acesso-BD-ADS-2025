using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestao_Escolar.DbContext;
using System.Linq.Expressions;
using System;
namespace Gestao_Escolar.Services
{
    public interface ITurmaService : IBaseService<Turma, TurmaDTO, TurmaCreateDTO, TurmaUpdateDTO>
    {
        Task<IEnumerable<TurmaDTO>> GetTurmasAtivasByPeriodoAsync(string periodo);
    }

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
            var turmas = await _context.Turmas.ToListAsync();
            return _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        }

        public async Task<TurmaDTO?> GetByIdAsync(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            return _mapper.Map<TurmaDTO>(turma);
        }

        public async Task<TurmaDTO> CreateAsync(TurmaCreateDTO createDto)
        {
            var turma = _mapper.Map<Turma>(createDto);
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
            return _mapper.Map<TurmaDTO>(turma);
        }

        public async Task<TurmaDTO?> UpdateAsync(int id, TurmaUpdateDTO updateDto)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null) return null;

            _mapper.Map(updateDto, turma);
            await _context.SaveChangesAsync();
            return _mapper.Map<TurmaDTO>(turma);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null) return false;

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TurmaDTO>> FindAsync(Expression<Func<Turma, bool>> predicate)
        {
            var turmas = await _context.Turmas.Where(predicate).ToListAsync();
            return _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        }

        /*public async Task<IEnumerable<TurmaDTO>> GetTurmasAtivasByPeriodoAsync(string periodo)
        {
            var turmas = await _context.Turmas
                .Where(t => t.Status == StatusTurma.Ativo && t.Periodo.ToString() == periodo)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        }*/

        public async Task<IEnumerable<TurmaDTO>> GetTurmasAtivasByPeriodoAsync(string periodo)
        {
            // Tenta converter a string 'periodo' para o enum PeriodoTurma
            if (!Enum.TryParse<PeriodoTurma>(periodo, true, out PeriodoTurma periodoEnum))
            {
                // Se a string não for um valor válido do enum, você pode:
                // 1. Lançar uma exceção (ex: ArgumentException)
                // 2. Retornar uma lista vazia
                // 3. Logar o erro
                // Por simplicidade, vamos retornar uma lista vazia aqui.
                return new List<TurmaDTO>();
            }

            var turmas = await _context.Turmas
                .Where(t => t.Status == StatusTurma.Ativo && t.Periodo == periodoEnum)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TurmaDTO>>(turmas);
        }

    }
}
    
