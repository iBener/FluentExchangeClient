using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestTicker : BinanceBaseFuturesRequest
{

    public BinanceFuturesRequestTicker(ExchangeOptions options) : this(String.Empty, String.Empty, options)
    {
    }

    public BinanceFuturesRequestTicker(string symbol, string quoteSymbol, ExchangeOptions options) : base(new { symbol = $"{symbol}{quoteSymbol}" }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/ticker/24hr" + QueryString);
        Weight = String.IsNullOrEmpty($"{symbol}{quoteSymbol}") ? 40 : 1;
    }

    public override int Weight { get; }
}
