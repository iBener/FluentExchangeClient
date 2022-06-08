using System;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestTicker : BinanceBaseRequest
{
    public BinanceRequestTicker(string? symbol = null, string? quoteSymbol = null) : base(new { symbol = symbol + quoteSymbol }, null)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/ticker/24hr" + QueryString);
    }
}
