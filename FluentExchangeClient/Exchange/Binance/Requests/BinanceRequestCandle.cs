using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestCandle : BinanceBaseRequest
{
    public BinanceRequestCandle(string symbol, string quoteSymbol, string interval, int limit = 500) 
        : base(new { symbol = $"{symbol}{quoteSymbol}", interval, limit }, null)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/klines" + QueryString);
    }
}
