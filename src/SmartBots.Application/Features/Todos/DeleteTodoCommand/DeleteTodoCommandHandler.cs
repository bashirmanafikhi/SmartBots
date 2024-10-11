using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;

namespace SmartBots.Application.Features.Todos
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTodoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.Repository<Todo>().GetByIdAsync(command.Id);
            if (todo == null)
            {
                return false;
            }
            await _unitOfWork.Repository<Todo>().DeleteAsync(todo);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
