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
    public BinanceFuturesRequestChangePositionMargin(string symbol, decimal amount, int type, long timestamp, ExchangeOptions options)
        : base(new { symbol, amount, type, timestamp }, options)
    {
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/positionMargin" + QueryString);
    }
}
