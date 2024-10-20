using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class AddExchangeCommandHandler : IRequestHandler<AddExchangeCommand, ExchangeAccountDto>
    {
        private readonly IExchangeAccountRepository _exchangeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddExchangeCommandHandler(IUnitOfWork unitOfWork, IExchangeAccountRepository exchangeRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
            _mapper = mapper;
        }

        public async Task<ExchangeAccountDto> Handle(AddExchangeCommand command, CancellationToken cancellationToken)
        {
            var exchangeAccount = new Domain.Entities.ExchangeAccount()
            {
                Name = command.Name,
                Type = command.Type,
                ApiKey = command.ApiKey,
                ApiSecret = command.ApiSecret,
                IsTest = command.IsTest,
            };

            await _exchangeRepository.AddAsync(exchangeAccount, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ExchangeAccountDto>(exchangeAccount);
        }
    }
}
