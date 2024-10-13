using SmartBots.Domain.Common;
using System.Linq.Expressions;

namespace SmartBots.Application.Common;
public abstract class BaseCriteria<TEntity> where TEntity : BaseEntity
{
    public abstract Expression<Func<TEntity, bool>> GetPredicateAsExpression();
}
