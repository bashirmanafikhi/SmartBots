using FluentValidation;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class AddTradingBotCommandValidator : AbstractValidator<AddTradingBotCommand>
{
    public AddTradingBotCommandValidator()
    {
        RuleFor(m => m.Model)
            .SetValidator(new TradingBotDtoValidator());
    }
}
