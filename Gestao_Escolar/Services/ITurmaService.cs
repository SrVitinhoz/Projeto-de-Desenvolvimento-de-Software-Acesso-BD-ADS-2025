
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoEscolar.Models;
using GestaoEscolar.DTOs;

namespace GestaoEscolar.Services
{
    public interface ITurmaService
    {
        Task<IEnumerable<TurmaDTO>> GetAllAsync();
        Task<TurmaDTO> GetByIdAsync(int id);
        Task<TurmaDTO> CreateAsync(TurmaCreateDTO turmaDTO);
        Task<TurmaDTO> UpdateAsync(int id, TurmaUpdateDTO turmaDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TurmaDTO>> GetByStatusAsync(string status);
        Task<IEnumerable<TurmaDTO>> GetByPeriodoAsync(string periodo);
    }
}
