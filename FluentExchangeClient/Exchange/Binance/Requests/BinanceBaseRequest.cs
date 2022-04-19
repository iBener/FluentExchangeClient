﻿using FluentExchangeClient.Builder;
using System;
using System.Globalization;
using System.Reflection;
using FluentExchangeClient.Common;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceBaseRequest : ExchangeRequestBase
{
    public override Uri BaseAddress => new("https://api.binance.com");

    public BinanceBaseRequest(object? param, ApiCredentials? credentials)
    {
        if (param != null)
        {
            foreach (var property in param.GetType().GetProperties())
            {
                object? value = property.GetValue(param);
                if (value != null)
                {
                    Query.Add(property.Name, value);
                }
            }
        }
        if (credentials != null)
        {
            Headers.Add("X-MBX-APIKEY", credentials.ApiKey);
            Query.Add("signature", Sign(credentials.Hash));
        }
    }
}
