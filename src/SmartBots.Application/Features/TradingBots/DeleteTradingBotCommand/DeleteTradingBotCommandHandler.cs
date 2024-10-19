using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class DeleteTradingBotCommandHandler : IRequestHandler<DeleteTradingBotCommand, bool>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ITradingBotRepository _tradingBotRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTradingBotCommandHandler(
        ICurrentUserService currentUserService,
        ITradingBotRepository tradingBotRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _tradingBotRepository = tradingBotRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTradingBotCommand request, CancellationToken cancellationToken)
    {
        var bot = await _tradingBotRepository.GetByIdAsync(request.Id);

        if (bot is null)
            return false; // throw NotFound ex

        var currentuserId = _currentUserService.GetUserId();
        bot.Authorize(currentuserId);

        await _tradingBotRepository.DeleteAsync(bot, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
