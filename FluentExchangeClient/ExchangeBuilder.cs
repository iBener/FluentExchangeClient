using FluentExchangeClient.Internal;
using FluentExchangeClient.Exchange;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using FluentExchangeClient.Exchange.Binance;

namespace FluentExchangeClient
{
    public class ExchangeBuilder
    {
        readonly ExchangeBuilderWithName builder;

        public ExchangeBuilder()
        {
            builder = new ExchangeBuilderWithName();
        }

        public ExchangeBuilderWithName UseBinance()
        {
            return builder.UseExchange(ExchangeNames.Binance);
        }

        public ExchangeBuilderWithName UseBitfinex()
        {
            return builder.UseExchange(ExchangeNames.Bitfinex);
        }

        public ExchangeBuilderWithName UseBittrex()
        {
            return builder.UseExchange(ExchangeNames.Bittrex);
        }

        public ExchangeBuilderWithName UsePoloniex()
        {
            return builder.UseExchange(ExchangeNames.Poloniex);
        }

        public ExchangeBuilderWithName UseCobinhood()
        {
            return builder.UseExchange(ExchangeNames.Cobinhood);
        }

        public ExchangeBuilderWithName UseExchange(string exchange)
        {
            return builder.UseExchange(exchange);
        }
    }

    public class ExchangeBuilderWithName
    {
        IExchangeBuilder builder;

        internal ExchangeBuilderWithName()
        {
        }

        internal ExchangeBuilderWithName UseExchange(string exchange)
        {
            switch (exchange)
            {
                case ExchangeNames.Binance:
                    builder = new BinanceExchangeBuilder();
                    break;
                case ExchangeNames.Bitfinex:
                case ExchangeNames.Bittrex:
                case ExchangeNames.Cobinhood:
                case ExchangeNames.Poloniex:
                    break;
                default:
                    throw new ExchangeClientException($"\"{exchange}\" is not supported. Supported exchanges:\n{ String.Join(", \n", ExchangeNames.List) }");
            }
            return this;
        }

        public ExchangeBuilderWithName UseHttp(HttpClient httpClient)
        {
            builder.Options.Http = httpClient;
            return this;
        }

        public ExchangeBuilderWithName SetCredentials(string apiKey, string apiSecret)
        {
            builder.SetCredentials(apiKey, apiSecret);
            return this;
        }

        public IExchange Build()
        {
            return builder.Build();
        }
    }
}
