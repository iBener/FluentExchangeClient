using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceDerivativeRequestChangeMargin : BinanceBaseDerivativeRequest
{
    public BinanceDerivativeRequestChangeMargin(string symbol, string marginType, long timestamp, ApiCredentials credentials)
        : base(new { symbol, marginType, timestamp }, credentials)
    {
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/marginType" + QueryString);
    }
}
