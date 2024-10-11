using MediatR;

namespace SmartBots.Application.Features.Todos
{
    public record GetAllTodosQuery : IRequest<List<TodoDto>>;
}
