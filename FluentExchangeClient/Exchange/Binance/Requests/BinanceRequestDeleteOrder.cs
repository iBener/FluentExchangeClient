using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestDeleteOrder : BinanceBaseRequest
{
    public BinanceRequestDeleteOrder(object param, ApiCredentials? credentials) : base(param, credentials)
    {
        Method = HttpMethod.Delete;
        RequestUri = new Uri(BaseAddress, $"/api/v3/order{ QueryString }");
    }
}
