using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestDeleteOrder : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestDeleteOrder(string symbol, string orderId, string origClientOrderId, ExchangeOptions options) :
        base(CreateParamObject(symbol, orderId, origClientOrderId, options.Timestamp), options)
    {
        Method = HttpMethod.Delete;
        RequestUri = new Uri(BaseAddress, $"/fapi/v1/order{ QueryString }");
    }

    private static object CreateParamObject(string symbol, string orderId, string origClientOrderId, long timeStamp)
    {
        return new
        {
            symbol,
            orderId,
            origClientOrderId,
            timeStamp
        };
    }
}
