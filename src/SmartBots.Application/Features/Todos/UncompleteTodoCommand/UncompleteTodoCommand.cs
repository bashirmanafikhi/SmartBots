using MediatR;

namespace SmartBots.Application.Features.Todos
{
    public record UncompleteTodoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
