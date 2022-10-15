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
    public BinanceFuturesRequestDeleteOrder(string symbol, string quoteSymbol, string orderId, string origClientOrderId, long timestamp, ExchangeOptions options) :
        base(CreateParamObject(symbol, quoteSymbol, orderId, origClientOrderId, timestamp), options)
    {
        Method = HttpMethod.Delete;
        RequestUri = new Uri(BaseAddress, $"/fapi/v1/order{ QueryString }");
    }

    private static object CreateParamObject(string symbol, string quoteSymbol, string orderId, string origClientOrderId, long timeStamp)
    {
        return new
        {
            symbol = $"{symbol}{quoteSymbol}",
            orderId,
            origClientOrderId,
            timeStamp
        };
    }
}
