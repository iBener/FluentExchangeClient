using System;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests
{
    class BinanceRequestTicker : BinanceBaseRequest
    {
        public BinanceRequestTicker() : base(null, null)
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri(BaseAddress, "/api/v3/ticker/price");
        }
    }
}
