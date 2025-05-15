
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoEscolar.Models;
using GestaoEscolar.DTOs;

namespace GestaoEscolar.Services
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioDTO>> GetAllAsync();
        Task<FuncionarioDTO> GetByIdAsync(int id);
        Task<FuncionarioDTO> CreateAsync(FuncionarioCreateDTO funcionarioDTO);
        Task<FuncionarioDTO> UpdateAsync(int id, FuncionarioUpdateDTO funcionarioDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<FuncionarioDTO>> GetByCargoAsync(string cargo);
        Task<IEnumerable<FuncionarioDTO>> GetByStatusAsync(string status);
        Task<FuncionarioDTO> GetByMatriculaAsync(int matricula);
    }
}
