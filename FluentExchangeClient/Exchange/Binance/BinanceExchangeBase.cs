using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

public abstract class BinanceExchangeBase : ExchangeBase
{
    internal BinanceExchangeBase(ExchangeOptions options) : base(options)
    {
    }

    private static double? serverTimeDiff = null;

    public override long Timestamp
    {
        get
        {
            double diff = 0;
            if (serverTimeDiff == null)
            {
                string? result = GetServerTime().GetAwaiter().GetResult();
                var response = JsonConvert.DeserializeObject<BinanceResponseServerTime>(result);
                if (response != null)
                {
                    serverTimeDiff = (response.serverTime - DateTimeOffset.UtcNow).TotalMilliseconds;
                    diff = serverTimeDiff.Value;
                }
            }
            else
            {
                diff = serverTimeDiff.Value;
            }
            var now = DateTimeOffset.UtcNow.AddMilliseconds(diff);
            return now.ToUnixTimeMilliseconds();
        }
    }

    internal IEnumerable<Trade>? GroupTrades(IEnumerable<BinanceResponseTrade>? trades)
    {
        if (trades == null)
        {
            return null;
        }

        var result = new List<Trade>();
        var orders = new Dictionary<string, Trade>();
        foreach (var tradeResponse in trades)
        {
            var trade = Map<Trade>(tradeResponse);
            if (trade == null)
            {
                continue;
            }
            if (!orders.ContainsKey(trade.OrderId))
            {
                trade.Commissions.Add(tradeResponse.commissionAsset, tradeResponse.commission);
                ((List<TradeTransaction>)trade.Transactions)
                    .Add(new TradeTransaction { Quantity = trade.Quantity, QuoteQuantity = trade.QuoteQuantity, Time = trade.Time });
                result.Add(trade);
                orders.Add(trade.OrderId, trade);
            }
            else
            {
                var mainTrade = orders[trade.OrderId];
                if (!mainTrade.Commissions.ContainsKey(tradeResponse.commissionAsset))
                {
                    mainTrade.Commissions.Add(tradeResponse.commissionAsset, tradeResponse.commission);
                }
                else
                {
                    mainTrade.Time = trade.Time;
                    mainTrade.Quantity += trade.Quantity;
                    mainTrade.QuoteQuantity += trade.QuoteQuantity;
                    mainTrade.Commissions[tradeResponse.commissionAsset] += tradeResponse.commission;
                    ((List<TradeTransaction>)mainTrade.Transactions)
                        .Add(new TradeTransaction { Quantity = trade.Quantity, QuoteQuantity = trade.QuoteQuantity, Time = trade.Time });
                }
            }
        }
        return result;
    }
}
