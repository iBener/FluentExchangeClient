#pragma warning disable CS0649
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using FluentExchangeClient.Mapper;
using Newtonsoft.Json;
using System;

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinanceResponseOrder
{
    public long orderId;
    public string clientOrderId;
    public string symbol;
    public string side;
    public decimal price;
    public decimal origQty;
    public decimal executedQty;
    public string type;
    public string status;
    public string timeInForce;
    [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
    public DateTimeOffset time;
}
