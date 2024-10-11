using FluentValidation;

namespace SmartBots.Application.Features.Todos
{
    public class AddTodoCommandValidator : AbstractValidator<AddTodoCommand>
    {
        public AddTodoCommandValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text is required")
                .MaximumLength(255)
                .WithMessage("Text cannot exceed 255 characters");
        }
    }
}
