using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class DeleteExchangeCommandHandler : IRequestHandler<DeleteExchangeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteExchangeCommandHandler(IUnitOfWork unitOfWork, IExchangeRepository exchangeRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteExchangeCommand command, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(command.Id);
            if (exchange == null)
            {
                return false; // Exchange not found
            }

            var currentuserid = _currentUserService.GetUserId();
            exchange.Authorize(currentuserid);

            await _exchangeRepository.DeleteAsync(exchange, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
