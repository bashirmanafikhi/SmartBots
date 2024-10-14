using MediatR;

namespace SmartBots.Application.Features.Todos;
public sealed class GetTodoQuery : IRequest<TodoDto>
{
    public Guid Id { get; set; }

    public GetTodoQuery(Guid id)
    {
        Id = id;
    }
}

