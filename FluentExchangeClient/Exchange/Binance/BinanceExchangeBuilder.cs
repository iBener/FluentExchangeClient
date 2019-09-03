using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FluentExchangeClient.Internal;
using FluentExchangeClient.Mapper;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Binance
{
    class BinanceExchangeBuilder : IExchangeBuilder
    {
        public ExchangeOptions Options { get; }

        public BinanceExchangeBuilder()
        {
            Options = new ExchangeOptions
            {
                ExchangeName = ExchangeNames.Binance,
                Mapper = MappingConfigurations.Binance.CreateMapper(),
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
