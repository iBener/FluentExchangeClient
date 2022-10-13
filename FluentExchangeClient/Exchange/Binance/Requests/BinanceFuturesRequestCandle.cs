using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestCandle : BinanceBaseFuturesRequest
{
    private readonly int limit;

    public BinanceFuturesRequestCandle(string symbol, string quoteSymbol, string interval, int limit, ExchangeOptions options)
        : base(new { symbol = symbol + quoteSymbol, interval, limit }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/klines" + QueryString);
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
