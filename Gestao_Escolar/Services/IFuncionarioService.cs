using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gestao_Escolar.DbContext;

namespace Gestao_Escolar.Services
{
    public interface IFuncionarioService : IBaseService<Funcionario, FuncionarioDTO, FuncionarioCreateDTO, FuncionarioUpdateDTO>
    {
        Task<IEnumerable<FuncionarioDTO>> GetProfessoresAtivosAsync();
        Task<bool> VerificarCpfExistenteAsync(string cpf);
    }

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
                .Include(f => f.Materia)
                .ToListAsync();
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
        }

        public async Task<FuncionarioDTO?> GetByIdAsync(int id)
        {
            var funcionario = await _context.Funcionarios
                .Include(f => f.Materia)
                .FirstOrDefaultAsync(f => f.Id == id);
            return _mapper.Map<FuncionarioDTO>(funcionario);
        }

        public async Task<FuncionarioDTO> CreateAsync(FuncionarioCreateDTO createDto)
        {
            // Verificar se CPF já existe
            if (await VerificarCpfExistenteAsync(createDto.Cpf))
            {
                throw new InvalidOperationException("CPF já cadastrado no sistema.");
            }

            var funcionario = _mapper.Map<Funcionario>(createDto);
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return _mapper.Map<FuncionarioDTO>(funcionario);
        }

        public async Task<FuncionarioDTO?> UpdateAsync(int id, FuncionarioUpdateDTO updateDto)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return null;

            _mapper.Map(updateDto, funcionario);
            await _context.SaveChangesAsync();
            return _mapper.Map<FuncionarioDTO>(funcionario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return false;

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FuncionarioDTO>> FindAsync(Expression<Func<Funcionario, bool>> predicate)
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Materia)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
        }

        public async Task<IEnumerable<FuncionarioDTO>> GetProfessoresAtivosAsync()
        {
            var professores = await _context.Funcionarios
                .Include(f => f.Materia)
                .Where(f => f.Cargo == CargoFuncionario.Professor && f.Status == StatusFuncionario.Ativo)
                .ToListAsync();
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(professores);
        }

        public async Task<bool> VerificarCpfExistenteAsync(string cpf)
        {
            return await _context.Funcionarios.AnyAsync(f => f.Cpf == cpf);
        }
    }
}