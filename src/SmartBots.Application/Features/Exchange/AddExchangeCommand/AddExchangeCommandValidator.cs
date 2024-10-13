using FluentValidation;

namespace SmartBots.Application.Features.Exchange
{
    public class AddExchangeCommandValidator : AbstractValidator<AddExchangeCommand>
    {
        public AddExchangeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(255)
                .WithMessage("Name cannot exceed 255 characters");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid exchange type");

            RuleFor(x => x.ApiKey)
                .NotEmpty()
                .WithMessage("API Key is required");

            RuleFor(x => x.ApiSecret)
                .NotEmpty()
                .WithMessage("API Secret is required");

            RuleFor(x => x.IsTest)
                .NotNull()
                .WithMessage("IsTest must be specified");
        }
    }
}
