using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class AddTradingBotCommandHandler : IRequestHandler<AddTradingBotCommand, Guid>
{
    private readonly ITradingBotRepository _tradingBotRepository;
    private readonly IExchangeAccountRepository _exchangeAccountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTradingBotCommandHandler(
        ITradingBotRepository tradingBotRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IExchangeAccountRepository exchangeRepository)
    {
        _tradingBotRepository = tradingBotRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _exchangeAccountRepository = exchangeRepository;
    }

    public async Task<Guid> Handle(AddTradingBotCommand request, CancellationToken cancellationToken)
    {
        var bot = _mapper.Map<TradingBot>(request.Model);
        var exchangeAccount = await _exchangeAccountRepository.GetByIdAsync(request.Model.ExchangeAccountId);

        if(exchangeAccount is null) 
        {
            // throw not found exception
        }

        bot.Id = Guid.NewGuid();
        bot.ExchangeAccount = exchangeAccount;

        await _tradingBotRepository.AddAsync(bot, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bot.Id;
    }
}
