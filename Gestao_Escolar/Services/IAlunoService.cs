
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoEscolar.Models;
using GestaoEscolar.DTOs;

namespace GestaoEscolar.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoDTO>> GetAllAsync();
        Task<AlunoDTO> GetByIdAsync(int id);
        Task<AlunoDTO> CreateAsync(AlunoCreateDTO alunoDTO);
        Task<AlunoDTO> UpdateAsync(int id, AlunoUpdateDTO alunoDTO);
        Task<bool> DeleteAsync(int id);
        Task<AlunoDTO> GetByMatriculaAsync(string matricula);
        Task<IEnumerable<AlunoDTO>> GetByTurmaAsync(int turmaId);
    }
}
