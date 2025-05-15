
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
    public class FuncionarioService : IFuncionarioService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FuncionarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetAllAsync()
        {
            var funcionarios = await _context.Funcionarios
                .AsNoTracking()
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
        }

        public async Task<FuncionarioDTO> GetByIdAsync(int id)
        {
            var funcionario = await _context.Funcionarios
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
            
            if (funcionario == null)
                return null;
            
            return _mapper.Map<FuncionarioDTO>(funcionario);
        }

        public async Task<FuncionarioDTO> CreateAsync(FuncionarioCreateDTO funcionarioDTO)
        {
            var funcionario = _mapper.Map<Funcionario>(funcionarioDTO);
            
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<FuncionarioDTO>(funcionario);
        }

        public async Task<FuncionarioDTO> UpdateAsync(int id, FuncionarioUpdateDTO funcionarioDTO)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            
            if (funcionario == null)
                return null;
            
            _mapper.Map(funcionarioDTO, funcionario);
            
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<FuncionarioDTO>(funcionario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            
            if (funcionario == null)
                return false;
            
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetByCargoAsync(string cargo)
        {
            var funcionarios = await _context.Funcionarios
                .AsNoTracking()
                .Where(f => f.Cargo == cargo)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetByStatusAsync(string status)
        {
            var funcionarios = await _context.Funcionarios
                .AsNoTracking()
                .Where(f => f.Status == status)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
        }

        public async Task<FuncionarioDTO> GetByMatriculaAsync(int matricula)
        {
            var funcionario = await _context.Funcionarios
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.MatriculaId == matricula);
            
            if (funcionario == null)
                return null;
            
            return _mapper.Map<FuncionarioDTO>(funcionario);
        }
    }
}
