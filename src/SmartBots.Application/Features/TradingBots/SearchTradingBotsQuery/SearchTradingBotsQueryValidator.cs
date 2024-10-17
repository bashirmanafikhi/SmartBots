using FluentValidation;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class SearchTradingBotsQueryValidator : AbstractValidator<SearchTradingBotsQuery>
{
    public SearchTradingBotsQueryValidator()
    {
        RuleFor(q => q.ExchangeId)
            .NotEmpty()
            .Must(id => id != Guid.Empty)
            .WithMessage("Exchange ID must be provided and cannot be an empty GUID.");
    }
}
