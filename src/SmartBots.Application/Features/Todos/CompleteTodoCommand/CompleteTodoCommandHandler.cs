using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;

namespace SmartBots.Application.Features.Todos
{
    public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITodoRepository _todoRepository;

        public CompleteTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(CompleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.Repository<Todo>().GetByIdAsync(command.Id);
            if (todo == null)
            {
                return false;
            }

            await _todoRepository.CompleteAsync(todo);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
