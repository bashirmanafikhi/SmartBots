using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.PlaceOrderCommand
{
    public record PlaceOrderCommand(Guid ExchangeId, OrderRequest OrderRequest) : IRequest<Order>;

}
