using SmartBots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeFactory
    {
        IExchangeClient CreateExchangeClient(Exchange exchange);
    }
}
