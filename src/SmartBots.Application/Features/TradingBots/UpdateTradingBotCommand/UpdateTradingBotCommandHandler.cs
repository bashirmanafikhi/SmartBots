﻿using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class UpdateTradingBotCommandHandler : IRequestHandler<UpdateTradingBotCommand, bool>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ITradingBotRepository _tradingBotRepository;
    private readonly IExchangeRepository _exchangeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTradingBotCommandHandler(
        ICurrentUserService currentUserService,
        ITradingBotRepository tradingBotRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IExchangeRepository exchangeRepository)
    {
        _currentUserService = currentUserService;
        _tradingBotRepository = tradingBotRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _exchangeRepository = exchangeRepository;
    }

    public async Task<bool> Handle(UpdateTradingBotCommand request, CancellationToken cancellationToken)
    {
        var bot = await _tradingBotRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bot is null)
            return false; // throw not found ex

        var exchange = await _exchangeRepository.GetByIdAsync(request.Model.ExchangeId, cancellationToken);

        if (exchange is null) 
            return false; // throw not found ex

        var currentUserId = _currentUserService.GetUserId();
        bot.Authorize(currentUserId);

        bot.Update(
            request.Model.Name,
            request.Model.BaseAsset,
            request.Model.QuoteAsset,
            request.Model.TradeSize,
            request.Model.BotType,
            request.Model.ExtraOrders,
            request.Model.StopLoss,
            request.Model.TakeProfit,
            exchange);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}