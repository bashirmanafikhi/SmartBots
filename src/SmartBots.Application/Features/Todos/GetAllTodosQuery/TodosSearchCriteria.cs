using SmartBots.Application.Common;
using SmartBots.Application.Common.Extensions;
using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Application.Features.Todos;
public sealed class TodosSearchCriteria : BaseCriteria<Todo>
{
    public string? Keyword { get; set; }
    public TodoPriority? Priority { get; set; }

    public override Expression<Func<Todo, bool>> GetPredicateAsExpression()
    {
        Expression<Func<Todo, bool>> predicate = x => true;

        if (!string.IsNullOrWhiteSpace(Keyword))
        {
            Expression<Func<Todo, bool>> keywordPredicate = x => x.Text.Contains(Keyword);
            predicate = predicate.And(keywordPredicate);
        }

        if (Priority.HasValue)
        {
            Expression<Func<Todo, bool>> priorityPredicate = x => x.Priority == Priority.Value;
            predicate = predicate.And(priorityPredicate);
        }

        return predicate;
    }
}


