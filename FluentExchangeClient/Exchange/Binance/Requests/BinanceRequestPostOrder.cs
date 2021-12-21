using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestPostOrder : BinanceBaseRequest
{
    public BinanceRequestPostOrder(object param, ApiCredentials credentials, bool test = false) : base(param, credentials)
    {
        string testOrder = test ? "/test" : "";
        Method = HttpMethod.Post;
        RequestUri = new Uri(BaseAddress, $"/api/v3/order{ testOrder }{ QueryString }");
    }
}
