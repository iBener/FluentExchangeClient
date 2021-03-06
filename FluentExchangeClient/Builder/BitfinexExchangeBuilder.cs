﻿using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Bitfinex;
using FluentExchangeClient.Mapper;
using System;

namespace FluentExchangeClient.Builder
{
    class BitfinexExchangeBuilder : IExchangeBuilder
    {
        public ExchangeOptions Options { get; }

        public BitfinexExchangeBuilder()
        {
            Options = new ExchangeOptions
            {
                ExchangeName = ExchangeNames.Bitfinex,
                Mapper = MappingConfigurations.Bitfinex.CreateMapper()
            };
        }

        public void SetCredentials(string apiKey, string apiSecret)
        {
            throw new NotImplementedException();
        }

        public IExchange Build()
        {
            //return new BitfinexExchange(Options);
            throw new NotImplementedException();
        }

        public IExchangeRaw BuildRaw()
        {
            //return new BitfinexExchange(Options);
            throw new NotImplementedException();
        }
    }
}
