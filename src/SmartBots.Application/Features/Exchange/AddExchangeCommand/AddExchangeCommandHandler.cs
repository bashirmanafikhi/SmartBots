using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class AddExchangeCommandHandler : IRequestHandler<AddExchangeCommand, ExchangeDto>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddExchangeCommandHandler(IUnitOfWork unitOfWork, IExchangeRepository exchangeRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
            _mapper = mapper;
        }

        public async Task<ExchangeDto> Handle(AddExchangeCommand command, CancellationToken cancellationToken)
        {
            var exchange = new Domain.Entities.Exchange()
            {
                Name = command.Name,
                Type = command.Type,
                ApiKey = command.ApiKey,
                ApiSecret = command.ApiSecret,
                IsTest = command.IsTest,
            };

            await _exchangeRepository.AddAsync(exchange, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ExchangeDto>(exchange);
        }
    }
}
