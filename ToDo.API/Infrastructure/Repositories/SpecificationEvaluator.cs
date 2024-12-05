using ToDo.API.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using ToDo.API.Domain.Core.Specifications;

namespace ToDo.API.Infrastructure.Repositories
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IBaseSpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = inputQuery.Where(specification.Criteria);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            return query;
        }
    }
}
