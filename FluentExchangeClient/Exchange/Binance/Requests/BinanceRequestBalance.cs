﻿using FluentExchangeClient.Builder;
using System;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestBalance : BinanceBaseRequest
{
    public BinanceRequestBalance(long timestamp, ExchangeOptions options) : base(new { timestamp }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/account" + QueryString);
    }
}
