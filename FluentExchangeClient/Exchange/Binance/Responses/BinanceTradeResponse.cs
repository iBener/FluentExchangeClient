#pragma warning disable CS0649

using FluentExchangeClient.Mapper;
using Newtonsoft.Json;
using System;

namespace FluentExchangeClient.Exchange.Binance.Responses
{
    class BinanceTradeResponse
    {
        public int id;
        public int orderId;
        public string symbol;
        public decimal price;
        public decimal qty;
        public decimal quoteQty;
        public decimal commission;
        public string commissionAsset;
        [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
        public DateTimeOffset time;
        public bool isBuyer;
        public bool isMaker;
        public bool isBestMatch;
    }
}