using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestBalance : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestBalance(long timestamp, ApiCredentials? credentials) : base(new { timestamp }, credentials)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v2/account" + QueryString);
    }
}
