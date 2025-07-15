using AutoMapper;
using Gestao_Escolar.DbContext;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Models;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gestao_Escolar.Services
{
    public interface IFuncionarioService : IBaseService<Funcionario, FuncionarioDTO, FuncionarioCreateDTO, FuncionarioUpdateDTO>
    {
        Task<IEnumerable<FuncionarioDTO>> GetProfessoresAtivosAsync();
        Task<bool> VerificarCpfExistenteAsync(string cpf);

        Task<FuncionarioLoginDTO?> GetLoginByCpfAsync(string cpf);
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
            var Funcionarios = await _context.Funcionarios
                .Include(f => f.Materia)
                .ToListAsync();
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(Funcionarios);
        }

        public async Task<FuncionarioDTO?> GetByIdAsync(int id)
        {
            var Funcionario = await _context.Funcionarios
                .Include(f => f.Materia)
                .FirstOrDefaultAsync(f => f.Id == id);
            return _mapper.Map<FuncionarioDTO>(Funcionario);
        }

        public async Task<FuncionarioDTO> CreateAsync(FuncionarioCreateDTO createDto)
        {

            if (await VerificarCpfExistenteAsync(createDto.Cpf))
            {
                throw new InvalidOperationException("CPF já cadastrado no sistema.");
            }

            var Funcionario = _mapper.Map<Funcionario>(createDto);
            _context.Funcionarios.Add(Funcionario);
            await _context.SaveChangesAsync();
            return _mapper.Map<FuncionarioDTO>(Funcionario);
        }

        public async Task<FuncionarioDTO?> UpdateAsync(int id, FuncionarioUpdateDTO updateDto)
        {
            var Funcionario = await _context.Funcionarios.FindAsync(id);
            if (Funcionario == null) return null;


            _mapper.Map(updateDto, Funcionario);


            if (!string.IsNullOrEmpty(updateDto.Senha))
            {
                Funcionario.Senha = updateDto.Senha;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<FuncionarioDTO>(Funcionario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Funcionario = await _context.Funcionarios.FindAsync(id);
            if (Funcionario == null) return false;

            _context.Funcionarios.Remove(Funcionario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FuncionarioDTO>> FindAsync(Expression<Func<Funcionario, bool>> predicate)
        {
            var Funcionarios = await _context.Funcionarios
                .Include(f => f.Materia)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(Funcionarios);
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

        public async Task<FuncionarioLoginDTO?> GetLoginByCpfAsync(string cpf)
        {
            var funcionario = await _context.Funcionarios
                .Where(f => f.Cpf == cpf)
                .Select(f => new FuncionarioLoginDTO
                {
                    Cpf = f.Cpf,
                    Senha = f.Senha
                })
                .FirstOrDefaultAsync();

            return funcionario;
        }
    }
}