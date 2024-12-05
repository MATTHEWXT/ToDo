using ToDo.API.Domain.Core.Models;
using ToDo.API.Domain.Core.Specifications;

namespace ToDo.API.Domain.Core.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IList<T>> ListAsync(IBaseSpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteRangeAsync(List<T> listItems);
    }
}
