using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Todos;

internal class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoDto>
{
    private readonly ITodoRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetTodoQueryHandler(ITodoRepository repository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<TodoDto> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id);

        var cuurentUserId = _currentUserService.GetUserId();

        todo.Authorize(cuurentUserId);

        return _mapper.Map<TodoDto>(todo);
    }
}

