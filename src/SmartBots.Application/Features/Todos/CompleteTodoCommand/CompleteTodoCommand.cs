using MediatR;

namespace SmartBots.Application.Features.Todos
{
    public record CompleteTodoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
