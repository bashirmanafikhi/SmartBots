using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetBalancesQuery
{
    public record GetBalancesQuery(Guid ExchangeAccountId) : IRequest<IEnumerable<Balance>>;
}
