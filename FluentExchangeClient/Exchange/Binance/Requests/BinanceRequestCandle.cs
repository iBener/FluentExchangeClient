using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestCandle : BinanceBaseRequest
{
    private readonly int limit;

    public BinanceRequestCandle(string symbol, string quoteSymbol, string interval, int limit = 500) 
        : base(new { symbol = symbol + quoteSymbol, interval, limit }, null)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/klines" + QueryString);
        this.limit = limit;
    }

    public override int Weight => limit switch
    {
        int i when i == 0 => 0,
        int i when i <= 100 => 1,
        int i when i > 100 && i <= 500 => 2,
        int i when i > 500 && i <= 1000 => 5,
        _ => 10
    };
}
