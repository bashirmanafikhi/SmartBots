using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class DeleteExchangeCommandHandler : IRequestHandler<DeleteExchangeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteExchangeCommandHandler(IUnitOfWork unitOfWork, IExchangeAccountRepository exchangeRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteExchangeCommand command, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeRepository.GetByIdAsync(command.Id);
            if (exchangeAccount == null)
            {
                return false; // Exchange Account not found
            }

            var currentuserid = _currentUserService.GetUserId();
            exchangeAccount.Authorize(currentuserid);

            await _exchangeRepository.DeleteAsync(exchangeAccount, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
