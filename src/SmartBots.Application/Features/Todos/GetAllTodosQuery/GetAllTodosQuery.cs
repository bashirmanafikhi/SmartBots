using MediatR;
using SmartBots.Application.Common;

namespace SmartBots.Application.Features.Todos;

public class GetAllTodosQuery : IRequest<PaginationResponse<TodoDto>>
{
    public Paging? Paging { get; set; }
    public TodosSearchCriteria Criteria { get; set; } = new TodosSearchCriteria();
}
