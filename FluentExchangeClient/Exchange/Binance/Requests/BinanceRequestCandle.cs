using System;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests
{
    class BinanceRequestCandle : BinanceBaseRequest
    {
        public BinanceRequestCandle() : base(null, null)
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri(BaseAddress, "/api/v1/klines"); ;
        }
    }
}
