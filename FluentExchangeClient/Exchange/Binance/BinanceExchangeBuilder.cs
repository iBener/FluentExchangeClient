using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Internal.Binance
{
    class BinanceExchangeBuilder : IExchangeBuilder
    {
        public ExchangeOptions Options { get; }

        public BinanceExchangeBuilder()
        {
            Options = new ExchangeOptions
            {
                ExchangeName = ExchangeNames.Binance
            };
        }

        public void SetCredentials(string apiKey, string apiSecret)
        {
            byte[] privateKey = Encoding.UTF8.GetBytes(apiSecret);
            Options.Credentials = new ApiCredentials
            {
                ApiKey = apiKey,
                Hash = new HMACSHA256(privateKey),
            };
        }

        public IExchange Build()
        {
            return new BinanceExchange(Options);
        }

    }
}
