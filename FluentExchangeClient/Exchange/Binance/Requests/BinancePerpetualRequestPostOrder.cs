using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinancePerpetualRequestPostOrder : BinanceBasePerpetualRequest
{
    public BinancePerpetualRequestPostOrder(object param, ApiCredentials credentials, bool test = false) : base(param, credentials)
    {
        string testOrder = test ? "/test" : "";
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, $"/fapi/v1/order{ testOrder }{ QueryString }");
    }
}
