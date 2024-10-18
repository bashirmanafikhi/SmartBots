using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.BinancePlatform
{
    public static class ConversionExtensions
    {
        public static Application.Interfaces.Balance ToBalance(this BinanceBalance balance)
        {
            return new Application.Interfaces.Balance
            {
                Asset = balance.Asset,
                Free = balance.Available,
                Locked = balance.Locked,
                Total = balance.Total
            };
        }

        public static Application.Interfaces.Order ToOrder(this BinanceOrderBase binanceOrder)
        {
            return new Application.Interfaces.Order
            {
                Id = binanceOrder.Id,
                Symbol = binanceOrder.Symbol,
                Side = binanceOrder.Side.ToOrderSide(),
                Type = binanceOrder.Type.ToOrderType(),
                Quantity = binanceOrder.Quantity,
                Price = binanceOrder.Price,
                Status = binanceOrder.Status.ToOrderStatus(),
                Filled = binanceOrder.QuantityFilled,
                TimeInForce = binanceOrder.TimeInForce.ToTimeInForce(),
                CreateTime = binanceOrder.CreateTime
            };
        }

        public static SmartBots.Domain.Enums.OrderSide ToOrderSide(this Binance.Net.Enums.OrderSide binanceSide) =>
            binanceSide switch
            {
                Binance.Net.Enums.OrderSide.Buy => SmartBots.Domain.Enums.OrderSide.BUY,
                Binance.Net.Enums.OrderSide.Sell => SmartBots.Domain.Enums.OrderSide.SELL,
                _ => throw new ArgumentException("Invalid Binance order side.")
            };

        public static OrderType ToOrderType(this SpotOrderType binanceType) =>
            binanceType switch
            {
                SpotOrderType.Limit => OrderType.LIMIT,
                SpotOrderType.Market => OrderType.MARKET,
                SpotOrderType.StopLoss or SpotOrderType.TakeProfit => OrderType.STOP,
                _ => throw new ArgumentException("Invalid Binance order type.")
            };

        public static SmartBots.Domain.Enums.OrderStatus ToOrderStatus(this Binance.Net.Enums.OrderStatus binanceStatus) =>
            binanceStatus switch
            {
                Binance.Net.Enums.OrderStatus.New => SmartBots.Domain.Enums.OrderStatus.NEW,
                Binance.Net.Enums.OrderStatus.PartiallyFilled => SmartBots.Domain.Enums.OrderStatus.PARTIALLY_FILLED,
                Binance.Net.Enums.OrderStatus.Filled => SmartBots.Domain.Enums.OrderStatus.FILLED,
                Binance.Net.Enums.OrderStatus.Canceled => SmartBots.Domain.Enums.OrderStatus.CANCELED,
                Binance.Net.Enums.OrderStatus.PendingCancel => SmartBots.Domain.Enums.OrderStatus.PENDING_CANCEL,
                Binance.Net.Enums.OrderStatus.Rejected => SmartBots.Domain.Enums.OrderStatus.REJECTED,
                Binance.Net.Enums.OrderStatus.Expired => SmartBots.Domain.Enums.OrderStatus.EXPIRED,
                _ => throw new ArgumentException("Invalid Binance order status.")
            };

        public static Binance.Net.Enums.OrderSide ToBinanceOrderSide(this SmartBots.Domain.Enums.OrderSide side) =>
            side switch
            {
                SmartBots.Domain.Enums.OrderSide.BUY => Binance.Net.Enums.OrderSide.Buy,
                SmartBots.Domain.Enums.OrderSide.SELL => Binance.Net.Enums.OrderSide.Sell,
                _ => throw new ArgumentException("Invalid order side.")
            };

        public static SpotOrderType ToBinanceSpotOrderType(this OrderType type) =>
            type switch
            {
                OrderType.LIMIT => SpotOrderType.Limit,
                OrderType.MARKET => SpotOrderType.Market,
                _ => throw new ArgumentException("Invalid order type.")
            };

        public static Binance.Net.Enums.TimeInForce ToBinanceTimeInForce(this SmartBots.Domain.Enums.TimeInForce timeInForce) =>
            timeInForce switch
            {
                SmartBots.Domain.Enums.TimeInForce.GTC => Binance.Net.Enums.TimeInForce.GoodTillCanceled,
                SmartBots.Domain.Enums.TimeInForce.IOC => Binance.Net.Enums.TimeInForce.ImmediateOrCancel,
                SmartBots.Domain.Enums.TimeInForce.FOK => Binance.Net.Enums.TimeInForce.FillOrKill,
                _ => throw new ArgumentException("Invalid time in force.")
            };

        public static SmartBots.Domain.Enums.TimeInForce ToTimeInForce(this Binance.Net.Enums.TimeInForce binanceTimeInForce) =>
            binanceTimeInForce switch
            {
                Binance.Net.Enums.TimeInForce.GoodTillCanceled => SmartBots.Domain.Enums.TimeInForce.GTC,
                Binance.Net.Enums.TimeInForce.ImmediateOrCancel => SmartBots.Domain.Enums.TimeInForce.IOC,
                Binance.Net.Enums.TimeInForce.FillOrKill => SmartBots.Domain.Enums.TimeInForce.FOK,
                _ => throw new ArgumentException("Invalid Binance time in force.")
            };

        public static Application.Interfaces.KlineInterval ToKlineInterval(this Binance.Net.Enums.KlineInterval interval)
        {
            switch (interval)
            {
                case Binance.Net.Enums.KlineInterval.OneSecond:
                    return Application.Interfaces.KlineInterval.OneSecond;
                case Binance.Net.Enums.KlineInterval.OneMinute:
                    return Application.Interfaces.KlineInterval.OneMinute;
                case Binance.Net.Enums.KlineInterval.ThreeMinutes:
                    return Application.Interfaces.KlineInterval.ThreeMinutes;
                case Binance.Net.Enums.KlineInterval.FiveMinutes:
                    return Application.Interfaces.KlineInterval.FiveMinutes;
                case Binance.Net.Enums.KlineInterval.FifteenMinutes:
                    return Application.Interfaces.KlineInterval.FifteenMinutes;
                case Binance.Net.Enums.KlineInterval.ThirtyMinutes:
                    return Application.Interfaces.KlineInterval.ThirtyMinutes;
                case Binance.Net.Enums.KlineInterval.OneHour:
                    return Application.Interfaces.KlineInterval.OneHour;
                case Binance.Net.Enums.KlineInterval.TwoHour:
                    return Application.Interfaces.KlineInterval.TwoHour;
                case Binance.Net.Enums.KlineInterval.FourHour:
                    return Application.Interfaces.KlineInterval.FourHour;
                case Binance.Net.Enums.KlineInterval.SixHour:
                    return Application.Interfaces.KlineInterval.SixHour;
                case Binance.Net.Enums.KlineInterval.EightHour:
                    return Application.Interfaces.KlineInterval.EightHour;
                case Binance.Net.Enums.KlineInterval.TwelveHour:
                    return Application.Interfaces.KlineInterval.TwelveHour;
                case Binance.Net.Enums.KlineInterval.OneDay:
                    return Application.Interfaces.KlineInterval.OneDay;
                case Binance.Net.Enums.KlineInterval.ThreeDay:
                    return Application.Interfaces.KlineInterval.ThreeDay;
                case Binance.Net.Enums.KlineInterval.OneWeek:
                    return Application.Interfaces.KlineInterval.OneWeek;
                case Binance.Net.Enums.KlineInterval.OneMonth:
                    return Application.Interfaces.KlineInterval.OneMonth;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, "Invalid KlineInterval value.");
            }
        }

        public static Binance.Net.Enums.KlineInterval ToBinanceKlineInterval(this Application.Interfaces.KlineInterval interval)
        {
            switch (interval)
            {
                case Application.Interfaces.KlineInterval.OneSecond:
                    return Binance.Net.Enums.KlineInterval.OneSecond;
                case Application.Interfaces.KlineInterval.OneMinute:
                    return Binance.Net.Enums.KlineInterval.OneMinute;
                case Application.Interfaces.KlineInterval.ThreeMinutes:
                    return Binance.Net.Enums.KlineInterval.ThreeMinutes;
                case Application.Interfaces.KlineInterval.FiveMinutes:
                    return Binance.Net.Enums.KlineInterval.FiveMinutes;
                case Application.Interfaces.KlineInterval.FifteenMinutes:
                    return Binance.Net.Enums.KlineInterval.FifteenMinutes;
                case Application.Interfaces.KlineInterval.ThirtyMinutes:
                    return Binance.Net.Enums.KlineInterval.ThirtyMinutes;
                case Application.Interfaces.KlineInterval.OneHour:
                    return Binance.Net.Enums.KlineInterval.OneHour;
                case Application.Interfaces.KlineInterval.TwoHour:
                    return Binance.Net.Enums.KlineInterval.TwoHour;
                case Application.Interfaces.KlineInterval.FourHour:
                    return Binance.Net.Enums.KlineInterval.FourHour;
                case Application.Interfaces.KlineInterval.SixHour:
                    return Binance.Net.Enums.KlineInterval.SixHour;
                case Application.Interfaces.KlineInterval.EightHour:
                    return Binance.Net.Enums.KlineInterval.EightHour;
                case Application.Interfaces.KlineInterval.TwelveHour:
                    return Binance.Net.Enums.KlineInterval.TwelveHour;
                case Application.Interfaces.KlineInterval.OneDay:
                    return Binance.Net.Enums.KlineInterval.OneDay;
                case Application.Interfaces.KlineInterval.ThreeDay:
                    return Binance.Net.Enums.KlineInterval.ThreeDay;
                case Application.Interfaces.KlineInterval.OneWeek:
                    return Binance.Net.Enums.KlineInterval.OneWeek;
                case Application.Interfaces.KlineInterval.OneMonth:
                    return Binance.Net.Enums.KlineInterval.OneMonth;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, "Invalid Application.Interfaces.KlineInterval value.");
            }
        }

        public static Application.Interfaces.Kline ToKline(this IBinanceKline k)
        {
            return new Application.Interfaces.Kline
            {
                OpenTime = k.OpenTime,
                Open = k.OpenPrice,
                High = k.HighPrice,
                Low = k.LowPrice,
                Close = k.ClosePrice,
                Volume = k.Volume,
                CloseTime = k.CloseTime
            };
        }

        public static Application.Interfaces.OrderBookEntry ToOrderBookEntry(this BinanceOrderBookEntry binanceEntry)
        {
            return new Application.Interfaces.OrderBookEntry
            {
                Price = binanceEntry.Price,
                Quantity = binanceEntry.Quantity
            };
        }

        public static TickerPrice ToTickerPrice(this BinancePrice symbol)
        {
            return new TickerPrice { 
                Symbol = symbol.Symbol, 
                Price = symbol.Price ,
                Timestamp = symbol.Timestamp
            };
        }
    }
}
