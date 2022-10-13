using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestGetOrder : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestGetOrder(string symbol, string quoteSymbol, string? orderId, string? clientOrderId, ExchangeOptions options) : 
        base(CreateParamObject(symbol, quoteSymbol, orderId, clientOrderId, options.Timestamp), options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/order" + QueryString);
    }

    private static object CreateParamObject(string symbol, string quoteSymbol, string? orderId, string? clientOrderId, long timestamp)
    {
        return new
        {
            symbol = $"{symbol}{quoteSymbol}",
            orderId,
            clientOrderId,
            timestamp
        };
    }
}
