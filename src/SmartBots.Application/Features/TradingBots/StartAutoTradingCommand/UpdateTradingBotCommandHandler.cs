using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class StartAutoTradingCommandHandler : IRequestHandler<StartAutoTradingCommand, bool>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ITradingBotRepository _tradingBotRepository;
    private readonly ITradingBotManager _tradingBotManager;

    public StartAutoTradingCommandHandler(
        ICurrentUserService currentUserService,
        ITradingBotRepository tradingBotRepository,
        ITradingBotManager tradingBotManager)
    {
        _currentUserService = currentUserService;
        _tradingBotRepository = tradingBotRepository;
        _tradingBotManager = tradingBotManager;
    }

    public async Task<bool> Handle(StartAutoTradingCommand request, CancellationToken cancellationToken)
    {
        var bot = await _tradingBotRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bot is null)
            return false; // throw not found ex

        var currentUserId = _currentUserService.GetUserId();
        bot.Authorize(currentUserId);

        await _tradingBotManager.StartBotAsync(bot, request.Interval, cancellationToken);

        return true;
    }
}
