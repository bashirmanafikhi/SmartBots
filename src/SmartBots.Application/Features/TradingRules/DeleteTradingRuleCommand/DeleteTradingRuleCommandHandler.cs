using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.TradingRules;
internal sealed class DeleteTradingRuleCommandHandler : IRequestHandler<DeleteTradingRuleCommand, bool>
{
    private readonly ITradingRuleRepository _tradingRuleRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTradingRuleCommandHandler(ITradingRuleRepository repository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _tradingRuleRepository = repository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTradingRuleCommand request, CancellationToken cancellationToken)
    {
        var tradingRule = await _tradingRuleRepository.GetByIdAsync(request.Id);
        if (tradingRule is null)
        {
            return false; //throw Not Found Exception
        }

        var currentuserId = _currentUserService.GetUserId();
        tradingRule.TradingBot.Authorize(currentuserId);

        await _tradingRuleRepository.DeleteAsync(tradingRule, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
