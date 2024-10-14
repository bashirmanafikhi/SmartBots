using FluentValidation;

namespace SmartBots.Application.Features.Todos;
internal class UpdateTodoCommandValidator : AbstractValidator<AddTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text is required")
            .MaximumLength(255)
            .WithMessage("Text cannot exceed 255 characters");
    }
}

