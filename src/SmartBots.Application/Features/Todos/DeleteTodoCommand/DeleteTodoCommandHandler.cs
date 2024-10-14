using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Todos
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITodoRepository _todoRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(command.Id);
            if (todo == null)
            {
                return false;
            }

            var currentuserId = _currentUserService.GetUserId();
            todo.Authorize(currentuserId);

            await _todoRepository.DeleteAsync(todo, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
