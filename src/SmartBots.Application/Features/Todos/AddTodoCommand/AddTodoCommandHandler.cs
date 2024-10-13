using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;

namespace SmartBots.Application.Features.Todos
{
    public class AddTodoCommandHandler(
        IUnitOfWork unitOfWork, 
        ITodoRepository todoRepository, 
        IMapper mapper,
        ICurrentUserService currentUserService) : IRequestHandler<AddTodoCommand, TodoDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITodoRepository _todoRepository = todoRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<TodoDto> Handle(AddTodoCommand command, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();

            if (!currentUserId.HasValue)
                throw new UnauthorizedAccessException();

            var todo = new Todo(currentUserId.Value, command.Text);

            await _todoRepository.AddAsync(todo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TodoDto>(todo);
        }
    }
}
