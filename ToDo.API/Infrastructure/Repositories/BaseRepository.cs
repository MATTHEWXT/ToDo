using Microsoft.EntityFrameworkCore;
using ToDo.API.Domain.Core.Models;
using ToDo.API.Domain.Core.Repositories;
using ToDo.API.Domain.Core.Specifications;
using ToDo.API.Domain.Entities;
using ToDo.API.Infrastructure.Data;

namespace ToDo.API.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteRangeAsync(List<T> listItems)
        {
            _dbContext.Set<T>().RemoveRange(listItems);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id) ?? throw new InvalidOperationException("Entity not found");
        }

        public async Task<IList<T>> ListAsync(IBaseSpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(IBaseSpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
