using MediatR;

namespace SmartBots.Application.Features.Todos;
public class UpdateTodoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public TodoDto Model { get; set; }
}

