using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestExchangeInfo : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestExchangeInfo() : base()
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/exchangeInfo");
    }
}
