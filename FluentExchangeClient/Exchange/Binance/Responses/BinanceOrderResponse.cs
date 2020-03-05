#pragma warning disable CS0649

using FluentExchangeClient.Mapper;
using Newtonsoft.Json;
using System;

namespace FluentExchangeClient.Exchange.Binance.Responses
{
    class BinanceOrderResponse
    {
        public string orderId;
        public string clientOrderId;
        public string symbol;
        public string side;
        public decimal price;
        public decimal origQty;
        public decimal executedQty;
        public string type;
        public string status;
        [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
        public DateTimeOffset time;
    }
}