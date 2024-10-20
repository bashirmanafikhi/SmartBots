using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi.GetAssetsQuery
{
    public record GetAssetsQuery(Guid ExchangeAccountId) : IRequest<IEnumerable<Asset>>;
}
