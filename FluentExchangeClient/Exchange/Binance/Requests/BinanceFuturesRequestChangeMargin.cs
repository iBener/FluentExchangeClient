using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestChangeMargin : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestChangeMargin(string symbol, string marginType, ExchangeOptions options)
        : base(new { symbol, marginType, options.Timestamp }, options)
    {
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/marginType" + QueryString);
    }
}
