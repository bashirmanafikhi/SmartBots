using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Todos
{
    public class UncompleteTodoCommandHandler : IRequestHandler<UncompleteTodoCommand, bool>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UncompleteTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UncompleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(command.Id, cancellationToken);
            if (todo == null)
                return false;

            var currentUserId = _currentUserService.GetUserId();
            todo.Authorize(currentUserId);

            todo.Uncomplete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
