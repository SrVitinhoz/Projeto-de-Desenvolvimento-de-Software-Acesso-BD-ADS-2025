using System.Linq.Expressions;

namespace Gestao_Escolar.Services.Interfaces
{
    public interface IBaseService<TEntity, TDto, TCreateDto, TUpdateDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TCreateDto createDto);
        Task<TDto?> UpdateAsync(int id, TUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}