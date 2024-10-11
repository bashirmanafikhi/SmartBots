using MediatR;

namespace SmartBots.Application.Features.Todos
{
    public record AddTodoCommand : IRequest<TodoDto>
    {
        public string Text { get; set; }
    }
}
