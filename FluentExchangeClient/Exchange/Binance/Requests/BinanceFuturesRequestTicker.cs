using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestTicker : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestTicker(string? symbol = null, string? quoteSymbol = null) : base(new { symbol = symbol + quoteSymbol }, null)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/ticker/24hr" + QueryString);
    }
}
