#pragma warning disable CS0649

using FluentExchangeClient.Mapper;
using Newtonsoft.Json;
using System;

namespace FluentExchangeClient.Exchange.Binance.Responses
{
    class BinanceServerTimeResponse
    {
        [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
        public DateTimeOffset serverTime;
    }
}
