using FluentValidation;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class SearchTradingBotsQueryValidator : AbstractValidator<SearchTradingBotsQuery>
{
    public SearchTradingBotsQueryValidator()
    {
        RuleFor(q => q.ExchangeAccountId)
            .NotEmpty()
            .Must(id => id != Guid.Empty)
            .WithMessage("Exchange Account ID must be provided and cannot be an empty GUID.");
    }
}
