using AutoMapper;
using FluentValidation;
using SmartBots.Application.Common;
using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Entities;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Features.TradingBots;
public class TradingBotDto : BaseDto, IMapFrom<TradingBot>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ExchangeAccountId { get; set; }
    public string ExchangeAccountName { get; set; }
    public string BaseAsset { get; set; }
    public string QuoteAsset { get; set; }
    public double TradeSize { get; set; }
    public BotType BotType { get; set; } = BotType.LONG;

    void IMapFrom<TradingBot>.Mapping(Profile profile)
    {
        profile.CreateMap<TradingBot, TradingBotDto>()
            .ForMember(dest => dest.ExchangeAccountName, opt => opt.MapFrom(src => src.ExchangeAccount.Name))
            .ReverseMap();
    }

    public ExtraOrdersSettings ExtraOrders { get; set; } = new ExtraOrdersSettings();
    public StopLossSettings StopLoss { get; set; } = new StopLossSettings();
    public TakeProfitSettings TakeProfit { get; set; } = new TakeProfitSettings();
}


public class TradingBotDtoValidator : AbstractValidator<TradingBotDto>
{
    public TradingBotDtoValidator()
    {

    }
}
