using FluentExchangeClient.Builder;
using System;

namespace FluentExchangeClient.Exchange.Binance.Requests
{
    class BinanceBaseRequest : ExchangeRequestBase
    {
        public override Uri BaseAddress => new Uri("https://api.binance.com");

        public BinanceBaseRequest(object param, ApiCredentials credentials)
        {
            if (param != null)
            {
                foreach (var property in param.GetType().GetProperties())
                {
                    var value = property.GetValue(param);
                    if (value != null && !String.IsNullOrEmpty(value.ToString()))
                    {
                        Query.Add(property.Name, value.ToString());
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
}
