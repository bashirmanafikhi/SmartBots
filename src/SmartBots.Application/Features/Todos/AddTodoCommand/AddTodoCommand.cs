using MediatR;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Features.Todos
{
    public record AddTodoCommand : IRequest<TodoDto>
    {
        public string Text { get; set; }
        public TodoPriority Priority { get; set; }
    }
}
