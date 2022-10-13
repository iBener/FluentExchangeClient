using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestCandle : BinanceBaseRequest
{
    public BinanceRequestCandle(string symbol, string quoteSymbol, string interval, int limit, ExchangeOptions options) 
        : base(new { symbol = $"{symbol}{quoteSymbol}", interval, limit }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/klines" + QueryString);
    }
}
