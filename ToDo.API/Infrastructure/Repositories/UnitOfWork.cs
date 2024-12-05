using ToDo.API.Domain.Core.Models;
using ToDo.API.Domain.Core.Repositories;
using ToDo.API.Infrastructure.Data;

namespace ToDo.API.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IDictionary<Type, dynamic> _repositories;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, dynamic>();
        }

        public IBaseRepository<T> Repository<T>() where T : BaseEntity
        {
            var entityType = typeof(T);

            if (_repositories.ContainsKey(entityType))
            {
                return _repositories[entityType];
            }

            var repositoryType = typeof(BaseRepository<>);

            var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

            if (repository == null)
            {
                throw new NullReferenceException("Repository should not be null");
            }

            _repositories.Add(entityType, repository);

            return (IBaseRepository<T>)repository;
        }
    }
}
