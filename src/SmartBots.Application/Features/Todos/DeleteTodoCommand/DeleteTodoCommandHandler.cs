using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Todos
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(command.Id);
            if (todo == null)
            {
                return false;
            }
            await _todoRepository.DeleteAsync(todo, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
