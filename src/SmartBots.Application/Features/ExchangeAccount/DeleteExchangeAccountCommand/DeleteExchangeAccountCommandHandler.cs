using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class DeleteExchangeAccountCommandHandler : IRequestHandler<DeleteExchangeAccountCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExchangeAccountRepository _exchangeAccountRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteExchangeAccountCommandHandler(IUnitOfWork unitOfWork, IExchangeAccountRepository exchangeRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _exchangeAccountRepository = exchangeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteExchangeAccountCommand command, CancellationToken cancellationToken)
        {
            var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(command.Id);
            if (exchangeAccount == null)
            {
                return false; // Exchange Account not found
            }

            var currentuserid = _currentUserService.GetUserId();
            exchangeAccount.Authorize(currentuserid);

            await _exchangeAccountRepository.DeleteAsync(exchangeAccount, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
