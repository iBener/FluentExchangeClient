using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceDerivativeRequestOpenOrders : BinanceBaseDerivativeRequest
{
    public BinanceDerivativeRequestOpenOrders(string symbol, string quoteSymbol, long timestamp, ApiCredentials credentials) :
        base(new { symbol = symbol + quoteSymbol, timestamp }, credentials)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/openOrders" + QueryString);
    }
}
