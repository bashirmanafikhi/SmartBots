using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Todos;
internal class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
{
    private readonly ITodoRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoCommandHandler(ITodoRepository repository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (todo is null)
            return false;

        var currentUserId = _currentUserService.GetUserId();
        todo.Authorize(currentUserId);

        todo.Update(request.Model.Text, request.Model.Priority);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
