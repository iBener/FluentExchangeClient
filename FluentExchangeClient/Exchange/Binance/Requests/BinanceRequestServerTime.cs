using FluentExchangeClient.Builder;
using System;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestServerTime : BinanceBaseRequest
{
    public BinanceRequestServerTime(ExchangeOptions options) : base(null, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/time");
    }
}
