using System.Linq.Expressions;

namespace SmartBots.Application.Common.Extensions;
public static class ExpressionExtensions
{
    public static Expression<T> Compose<T>(
        this Expression<T> first,
        Expression<T> second,
        Func<Expression, Expression, Expression> merge)
    {
        // Map the parameters from the second expression to the first
        var parameterMap = first.Parameters
            .Select((firstParam, index) => new { firstParam, secondParam = second.Parameters[index] })
            .ToDictionary(p => p.secondParam, p => p.firstParam);

        // Replace parameters in the second expression with parameters from the first
        var secondBody = ParameterRebinder.ReplaceParameters(parameterMap, second.Body);

        // Merge the two expression bodies
        return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
    }

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        return first.Compose(second, Expression.AndAlso);
    }
}

// Helper class to replace parameters
public class ParameterRebinder : ExpressionVisitor
{
    private readonly Dictionary<ParameterExpression, ParameterExpression> _parameterMap;

    public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> parameterMap)
    {
        _parameterMap = parameterMap ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }

    public static Expression ReplaceParameters(
        Dictionary<ParameterExpression, ParameterExpression> map,
        Expression exp)
    {
        return new ParameterRebinder(map).Visit(exp);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (_parameterMap.TryGetValue(node, out var replacement))
        {
            node = replacement;
        }

        return base.VisitParameter(node);
    }
}

