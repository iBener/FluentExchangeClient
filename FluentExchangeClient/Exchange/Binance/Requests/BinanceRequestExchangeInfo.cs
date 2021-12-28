using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestExchangeInfo : BinanceBaseRequest
{
    public BinanceRequestExchangeInfo() : base(null, null)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/exchangeInfo");
    }
}
