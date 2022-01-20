using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceDerivativeRequestCandle : BinanceBaseDerivativeRequest
{
    public BinanceDerivativeRequestCandle(string symbol, string quoteSymbol, string interval, int limit = 500)
    : base(new { symbol = symbol + quoteSymbol, interval, limit }, null)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/klines" + QueryString);
    }
}
