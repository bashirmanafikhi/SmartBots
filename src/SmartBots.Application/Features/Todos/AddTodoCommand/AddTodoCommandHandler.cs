using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;

namespace SmartBots.Application.Features.Todos
{
    public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, TodoDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AddTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TodoDto> Handle(AddTodoCommand command, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();

            if (!currentUserId.HasValue)
                throw new UnauthorizedAccessException();

            var todo = new Todo(currentUserId.Value.ToString(), command.Text, command.Priority);

            await _todoRepository.AddAsync(todo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TodoDto>(todo);
        }
    }

}
