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
        private readonly ICurrentUserService _currentUserService;

        public AddExchangeCommandHandler(IUnitOfWork unitOfWork, IExchangeRepository exchangeRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ExchangeDto> Handle(AddExchangeCommand command, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();

            if (!currentUserId.HasValue)
                throw new UnauthorizedAccessException();

            var exchange = new Domain.Entities.Exchange()
            {
                Name = command.Name,
                Type = command.Type,
                ApiKey = command.ApiKey,
                ApiSecret = command.ApiSecret,
                IsTest = command.IsTest,
                ApplicationUserId = currentUserId.ToString()!,
            };

            await _exchangeRepository.AddAsync(exchange, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ExchangeDto>(exchange);
        }
    }
}
