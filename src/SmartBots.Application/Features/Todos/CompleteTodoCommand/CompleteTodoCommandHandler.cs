﻿using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Todos
{
    public class CompleteTodoCommandHandler(
        IUnitOfWork unitOfWork,
        ITodoRepository todoRepository,
        ICurrentUserService currentUserService) : IRequestHandler<CompleteTodoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITodoRepository _todoRepository = todoRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<bool> Handle(CompleteTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(command.Id, cancellationToken);
            if (todo == null)
                return false;

            var currentUserId = _currentUserService.GetUserId();
            todo.Authorize(currentUserId);

            todo.Complete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
