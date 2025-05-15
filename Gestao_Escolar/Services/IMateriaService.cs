
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoEscolar.Models;
using GestaoEscolar.DTOs;

namespace GestaoEscolar.Services
{
    public interface IMateriaService
    {
        Task<IEnumerable<MateriaDTO>> GetAllAsync();
        Task<MateriaDTO> GetByIdAsync(int id);
        Task<MateriaDTO> CreateAsync(MateriaCreateDTO materiaDTO);
        Task<MateriaDTO> UpdateAsync(int id, MateriaUpdateDTO materiaDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MateriaDTO>> GetByFuncionarioAsync(int funcionarioId);
    }
}
