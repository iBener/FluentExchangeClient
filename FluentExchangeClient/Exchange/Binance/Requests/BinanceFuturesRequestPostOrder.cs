using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestPostOrder : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestPostOrder(object param, ExchangeOptions options) : base(param, options)
    {
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, $"/fapi/v1/order{ QueryString }");
    }
}
