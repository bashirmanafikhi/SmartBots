using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using SmartBots.Application.Interfaces;

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

        public static Application.Interfaces.OrderSide ToOrderSide(this Binance.Net.Enums.OrderSide binanceSide) =>
            binanceSide switch
            {
                Binance.Net.Enums.OrderSide.Buy => Application.Interfaces.OrderSide.BUY,
                Binance.Net.Enums.OrderSide.Sell => Application.Interfaces.OrderSide.SELL,
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

        public static Application.Interfaces.OrderStatus ToOrderStatus(this Binance.Net.Enums.OrderStatus binanceStatus) =>
            binanceStatus switch
            {
                Binance.Net.Enums.OrderStatus.New => Application.Interfaces.OrderStatus.NEW,
                Binance.Net.Enums.OrderStatus.PartiallyFilled => Application.Interfaces.OrderStatus.PARTIALLY_FILLED,
                Binance.Net.Enums.OrderStatus.Filled => Application.Interfaces.OrderStatus.FILLED,
                Binance.Net.Enums.OrderStatus.Canceled => Application.Interfaces.OrderStatus.CANCELED,
                Binance.Net.Enums.OrderStatus.PendingCancel => Application.Interfaces.OrderStatus.PENDING_CANCEL,
                Binance.Net.Enums.OrderStatus.Rejected => Application.Interfaces.OrderStatus.REJECTED,
                Binance.Net.Enums.OrderStatus.Expired => Application.Interfaces.OrderStatus.EXPIRED,
                _ => throw new ArgumentException("Invalid Binance order status.")
            };

        public static Binance.Net.Enums.OrderSide ToBinanceOrderSide(this Application.Interfaces.OrderSide side) =>
            side switch
            {
                Application.Interfaces.OrderSide.BUY => Binance.Net.Enums.OrderSide.Buy,
                Application.Interfaces.OrderSide.SELL => Binance.Net.Enums.OrderSide.Sell,
                _ => throw new ArgumentException("Invalid order side.")
            };

        public static SpotOrderType ToBinanceSpotOrderType(this OrderType type) =>
            type switch
            {
                OrderType.LIMIT => SpotOrderType.Limit,
                OrderType.MARKET => SpotOrderType.Market,
                _ => throw new ArgumentException("Invalid order type.")
            };

        public static Binance.Net.Enums.TimeInForce ToBinanceTimeInForce(this Application.Interfaces.TimeInForce timeInForce) =>
            timeInForce switch
            {
                Application.Interfaces.TimeInForce.GTC => Binance.Net.Enums.TimeInForce.GoodTillCanceled,
                Application.Interfaces.TimeInForce.IOC => Binance.Net.Enums.TimeInForce.ImmediateOrCancel,
                Application.Interfaces.TimeInForce.FOK => Binance.Net.Enums.TimeInForce.FillOrKill,
                _ => throw new ArgumentException("Invalid time in force.")
            };

        public static Application.Interfaces.TimeInForce ToTimeInForce(this Binance.Net.Enums.TimeInForce binanceTimeInForce) =>
            binanceTimeInForce switch
            {
                Binance.Net.Enums.TimeInForce.GoodTillCanceled => Application.Interfaces.TimeInForce.GTC,
                Binance.Net.Enums.TimeInForce.ImmediateOrCancel => Application.Interfaces.TimeInForce.IOC,
                Binance.Net.Enums.TimeInForce.FillOrKill => Application.Interfaces.TimeInForce.FOK,
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
                OpenPrice = k.OpenPrice,
                HighPrice = k.HighPrice,
                LowPrice = k.LowPrice,
                ClosePrice = k.ClosePrice,
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
            return new TickerPrice
            {
                Symbol = symbol.Symbol,
                Price = symbol.Price,
                Timestamp = symbol.Timestamp
            };
        }

        public static KlineUpdateData ToKlineData(this IBinanceStreamKlineData binanceKline)
        {
            return new KlineUpdateData
            {
                Symbol = binanceKline.Symbol,
                Interval = (Application.Interfaces.KlineInterval)binanceKline.Data.Interval,
                OpenTime = binanceKline.Data.OpenTime,
                OpenPrice = binanceKline.Data.OpenPrice,
                HighPrice = binanceKline.Data.HighPrice,
                LowPrice = binanceKline.Data.LowPrice,
                ClosePrice = binanceKline.Data.ClosePrice,
                Volume = binanceKline.Data.Volume,
                CloseTime = binanceKline.Data.CloseTime,
            };
        }

        public static TickerData ToTickerData(this IBinanceTick binanceTick)
        {
            return new TickerData
            {
                Symbol = binanceTick.Symbol,
                LastPrice = binanceTick.LastPrice,
            };
        }
    }
}
