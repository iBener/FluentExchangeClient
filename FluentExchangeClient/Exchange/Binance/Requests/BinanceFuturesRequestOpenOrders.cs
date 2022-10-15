using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestOpenOrders : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestOpenOrders(string symbol, string quoteSymbol, long timestamp, ExchangeOptions options) :
        base(new { symbol = symbol + quoteSymbol, timestamp }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/openOrders" + QueryString);
    }
}
