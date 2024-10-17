using SmartBots.Application.Common;
using SmartBots.Application.Common.Extensions;
using SmartBots.Domain.Entities;
using System.Linq.Expressions;

namespace SmartBots.Application.Features.TradingBots;
public sealed class TradingBotsSearchCriteria : BaseCriteria<TradingBot>
{
    public string? Keyword { get; set; }

    public override Expression<Func<TradingBot, bool>> GetPredicateAsExpression()
    {
        Expression<Func<TradingBot, bool>> predicate = x => true;

        if (!string.IsNullOrWhiteSpace(Keyword))
        {
            Expression<Func<TradingBot, bool>> keywordPredicate = x => x.Name.Contains(Keyword);
            predicate = predicate.And(keywordPredicate);
        }

        return predicate;
    }
}
