using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Features.Exchange;
public sealed class UpdateExchangeAccountCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public ExchangeAccountDto Model { get; set; }
}
