using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.Exchange;
internal sealed class UpdateExchangeAccountCommandHandler : IRequestHandler<UpdateExchangeAccountCommand, bool>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IExchangeAccountRepository _exchangeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateExchangeAccountCommandHandler(
        ICurrentUserService currentUserService, 
        IExchangeAccountRepository exchangeRepository, 
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _exchangeRepository = exchangeRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> Handle(UpdateExchangeAccountCommand request, CancellationToken cancellationToken)
    {
        var exchangeAccount = await _exchangeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (exchangeAccount is null)
            return false; // throw not found ex

        var currentUserId = _currentUserService.GetUserId();
        exchangeAccount.Authorize(currentUserId);

        exchangeAccount.Update(
            request.Model.Name,
            request.Model.ApiKey,
            request.Model.ApiSecret,
            request.Model.IsTest,
            request.Model.Type);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
