using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class DeleteExchangeCommandHandler : IRequestHandler<DeleteExchangeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExchangeRepository _exchangeRepository;

        public DeleteExchangeCommandHandler(IUnitOfWork unitOfWork, IExchangeRepository exchangeRepository)
        {
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
        }

        public async Task<bool> Handle(DeleteExchangeCommand command, CancellationToken cancellationToken)
        {
            var exchange = await _exchangeRepository.GetByIdAsync(command.Id);
            if (exchange == null)
            {
                return false; // Exchange not found
            }

            await _exchangeRepository.DeleteAsync(exchange, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
