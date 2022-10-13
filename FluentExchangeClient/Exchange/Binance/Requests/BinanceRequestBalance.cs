using FluentExchangeClient.Builder;
using System;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestBalance : BinanceBaseRequest
{
    public BinanceRequestBalance(ExchangeOptions options) : base(new { options.Timestamp }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/account" + QueryString);
    }
}
