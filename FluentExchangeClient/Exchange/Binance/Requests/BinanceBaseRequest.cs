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
                    if (IsNotDefault(value))
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

        public bool IsNotDefault(object value)
        {
            Type type = value?.GetType();
            if (type == typeof(string))
            {
                return !String.IsNullOrEmpty(value?.ToString());
            }
            if (type != null && type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return !value.Equals(defaultValue);
            }
            return value != null;
        }
    }
}
