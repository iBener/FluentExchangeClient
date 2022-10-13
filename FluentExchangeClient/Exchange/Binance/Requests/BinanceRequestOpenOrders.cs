using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestOpenOrders : BinanceBaseRequest
{
    public BinanceRequestOpenOrders(string? symbol, string? quoteSymbol, ExchangeOptions options) :
        base(new { symbol = symbol + quoteSymbol, options.Timestamp }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/openOrders" + QueryString);
    }
}
