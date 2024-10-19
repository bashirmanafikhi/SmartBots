using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class AddTradingBotCommandHandler : IRequestHandler<AddTradingBotCommand, Guid>
{
    private readonly ITradingBotRepository _tradingBotRepository;
    private readonly IExchangeRepository _exchangeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTradingBotCommandHandler(
        ITradingBotRepository tradingBotRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IExchangeRepository exchangeRepository)
    {
        _tradingBotRepository = tradingBotRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _exchangeRepository = exchangeRepository;
    }

    public async Task<Guid> Handle(AddTradingBotCommand request, CancellationToken cancellationToken)
    {
        var bot = _mapper.Map<TradingBot>(request.Model);
        var exchange = await _exchangeRepository.GetByIdAsync(request.Model.ExchangeId);

        if(exchange is null) 
        {
            // throw not found exception
        }

        bot.Id = Guid.NewGuid();
        bot.Exchange = exchange;

        await _tradingBotRepository.AddAsync(bot, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bot.Id;
    }
}
