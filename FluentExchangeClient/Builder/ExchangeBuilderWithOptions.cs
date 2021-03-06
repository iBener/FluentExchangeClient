﻿using FluentExchangeClient.Exchange;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentExchangeClient.Builder
{
    public class ExchangeBuilderWithOptions
    {
        IExchangeBuilder builder;

        public ExchangeBuilderWithOptions(string exchangeName)
        {
            UseExchange(exchangeName);
        }

        public ExchangeBuilderWithOptions(string exchangeName, HttpClient http)
        {
            UseExchange(exchangeName);
            UseHttp(http);
        }

        public ExchangeBuilderWithOptions(string exchangeName, string apiKey, string apiSecret)
        {
            UseExchange(exchangeName);
            SetCredentials(apiKey, apiSecret);
        }

        public ExchangeBuilderWithOptions(string exchangeName, string apiKey, string apiSecret, HttpClient http)
        {
            UseExchange(exchangeName);
            SetCredentials(apiKey, apiSecret);
            UseHttp(http);
        }

        internal ExchangeBuilderWithOptions UseExchange(string exchange)
        {
            switch (exchange)
            {
                case ExchangeNames.Binance:
                    builder = new BinanceExchangeBuilder();
                    break;
                case ExchangeNames.Bitfinex:
                    builder = new BitfinexExchangeBuilder();
                    break;
                case ExchangeNames.Bittrex:
                    builder = new BittrexExchangeBuilder();
                    break;
                case ExchangeNames.Cobinhood:
                    builder = new CobinhoodExchangeBuilder();
                    break;
                case ExchangeNames.Poloniex:
                    builder = new PoloniexExchangeBuilder();
                    break;
                default:
                    throw new ExchangeClientException($"\"{exchange}\" is not supported. Supported exchanges:\n{ String.Join(", \n", ExchangeNames.List) }");
            }
            return this;
        }

        public ExchangeBuilderWithOptions UseHttp(HttpClient httpClient)
        {
            builder.Options.Http = httpClient;
            return this;
        }

        public ExchangeBuilderWithOptions SetCredentials(string apiKey, string apiSecret)
        {
            builder.SetCredentials(apiKey, apiSecret);
            return this;
        }

        public ExchangeBuilderWithOptions UseNormalizedSymbols()
        {
            throw new NotImplementedException("Normalized symbol option doesn't implemented yet");
        }

        public ExchangeBuilderWithOptions UseExchangeSymbols()
        {
            throw new NotImplementedException("Exchange symbol option doesn't implemented yet");
        }

        public IExchange Build()
        {
            return builder.Build();
        }

        public IExchangeRaw BuildRaw()
        {
            return builder.BuildRaw();
        }
    }
}
