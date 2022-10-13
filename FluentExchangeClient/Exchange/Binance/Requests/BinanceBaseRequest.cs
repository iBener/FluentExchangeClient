using FluentExchangeClient.Builder;
using System;
using System.Globalization;
using System.Reflection;
using FluentExchangeClient.Common;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceBaseRequest : ExchangeRequestBase
{
    public override Uri BaseAddress { get; }

    public BinanceBaseRequest(object? param, ExchangeOptions options)
    {
        if (options.UseTestServer)
        {
            BaseAddress = new("https://testnet.binance.vision/api");
        }
        else
        {
            BaseAddress = new("https://api.binance.com");
        }
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
        if (options.Credentials?.Hash != null && !String.IsNullOrEmpty(options.Credentials?.ApiKey))
        {
            Query.Add("signature", Sign(options.Credentials.Hash));
            Headers.Add("X-MBX-APIKEY", options.Credentials.ApiKey);
        }
    }
}
