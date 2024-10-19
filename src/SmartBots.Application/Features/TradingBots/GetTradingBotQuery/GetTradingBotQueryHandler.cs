using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class GetTradingBotQueryHandler : IRequestHandler<GetTradingBotQuery, TradingBotDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ITradingBotRepository _tradingBotRepository;
    private readonly IMapper _mapper;

    public GetTradingBotQueryHandler(ICurrentUserService currentUserService, ITradingBotRepository tradingBotRepository, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _tradingBotRepository = tradingBotRepository;
        _mapper = mapper;
    }

    public async Task<TradingBotDto> Handle(GetTradingBotQuery request, CancellationToken cancellationToken)
    {
        var bot = await _tradingBotRepository.GetByIdAsync(request.Id);

        var cuurentUserId = _currentUserService.GetUserId();

        bot.Authorize(cuurentUserId);

        return _mapper.Map<TradingBotDto>(bot);
    }
}
