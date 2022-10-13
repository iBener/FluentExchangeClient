using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestChangePositionMargin : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestChangePositionMargin(string symbol, decimal amount, int type, ExchangeOptions options)
        : base(new { symbol, amount, type, options.Timestamp }, options)
    {
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/positionMargin" + QueryString);
    }
}
