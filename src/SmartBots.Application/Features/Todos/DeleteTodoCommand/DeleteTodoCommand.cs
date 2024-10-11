using MediatR;

namespace SmartBots.Application.Features.Todos
{
    public record DeleteTodoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
