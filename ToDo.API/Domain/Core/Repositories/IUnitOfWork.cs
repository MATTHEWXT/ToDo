using ToDo.API.Domain.Core.Models;

namespace ToDo.API.Domain.Core.Repositories
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> Repository<T>() where T : BaseEntity;
    }
}
