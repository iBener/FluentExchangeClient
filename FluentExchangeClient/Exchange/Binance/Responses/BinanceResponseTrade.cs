#pragma warning disable CS0649

using FluentExchangeClient.Mapper;
using Newtonsoft.Json;
using System;

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinanceResponseTrade
{
    public long id;
    public long orderId;
    public string symbol;
    public decimal price;
    public decimal qty;
    public decimal quoteQty;
    public decimal commission;
    public string commissionAsset;
    [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
    public DateTimeOffset time;
    public bool isBuyer; // TODO: buyer for perpetual
    public bool isMaker; // TODO: maker for perpetual
    public bool isBestMatch;
    public string side;
    public decimal realizedPnl;
}
