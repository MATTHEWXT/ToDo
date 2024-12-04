using System.Linq.Expressions;

namespace ToDo.API.Domain.Core.Repositories
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>>? GroupBy { get; private set; }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public virtual void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        public virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}
