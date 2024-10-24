using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Features.ExchangeApi.SubscribeToUserDataUpdatesRequest
{
    public class SubscribeToUserDataUpdatesRequest : IRequest<bool>
    {
        public Guid ExchangeAccountId { get; set; }
        public Action<OrderUpdateData> OnOrderUpdate { get; set; }
        public Action<OcoOrderUpdateData> OnOcoOrderUpdate { get; set; }
        public Action<AccountPositionUpdateData> OnAccountPositionUpdate { get; set; }
        public Action<BalanceUpdateData> OnBalanceUpdate { get; set; }
        public Action OnListenKeyExpired { get; set; }

        public SubscribeToUserDataUpdatesRequest(
            Guid exchangeAccountId,
            Action<OrderUpdateData> onOrderUpdate,
            Action<OcoOrderUpdateData> onOcoOrderUpdate,
            Action<AccountPositionUpdateData> onAccountPositionUpdate,
            Action<BalanceUpdateData> onBalanceUpdate,
            Action onListenKeyExpired)
        {
            ExchangeAccountId = exchangeAccountId;
            OnOrderUpdate = onOrderUpdate;
            OnOcoOrderUpdate = onOcoOrderUpdate;
            OnAccountPositionUpdate = onAccountPositionUpdate;
            OnBalanceUpdate = onBalanceUpdate;
            OnListenKeyExpired = onListenKeyExpired;
        }
    }
}
