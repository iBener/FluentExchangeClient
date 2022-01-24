using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestChangeLeverage : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestChangeLeverage(string symbol, int leverage, long timestamp, ApiCredentials credentials) 
        : base(new { symbol, leverage, timestamp }, credentials)
    {
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/leverage" + QueryString);
    }
}
