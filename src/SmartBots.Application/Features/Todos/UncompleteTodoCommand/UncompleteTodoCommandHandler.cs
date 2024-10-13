using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Todos
{
    public class UncompleteTodoCommandHandler : IRequestHandler<UncompleteTodoCommand, bool>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UncompleteTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(UncompleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(command.Id, cancellationToken);
            if (todo == null)
                return false;

            todo.Uncomplete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
