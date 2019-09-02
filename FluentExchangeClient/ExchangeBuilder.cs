using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Binance;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FluentExchangeClient
{
    public class ExchangeBuilder
    {
        readonly ExchangeBuilderWithOptions builder;

        public ExchangeBuilder()
        {
            builder = new ExchangeBuilderWithOptions();
        }

        public ExchangeBuilderWithOptions UseBinance()
        {
            return builder.UseExchange(ExchangeNames.Binance);
        }

        public ExchangeBuilderWithOptions UseBitfinex()
        {
            return builder.UseExchange(ExchangeNames.Bitfinex);
        }

        public ExchangeBuilderWithOptions UseBittrex()
        {
            return builder.UseExchange(ExchangeNames.Bittrex);
        }

        public ExchangeBuilderWithOptions UsePoloniex()
        {
            return builder.UseExchange(ExchangeNames.Poloniex);
        }

        public ExchangeBuilderWithOptions UseCobinhood()
        {
            return builder.UseExchange(ExchangeNames.Cobinhood);
        }

        public ExchangeBuilderWithOptions UseExchange(string exchange)
        {
            return builder.UseExchange(exchange);
        }
    }

    public class ExchangeBuilderWithOptions
    {
        readonly ExchangeOptions options;

        internal ExchangeBuilderWithOptions()
        {
            options = new ExchangeOptions();
        }

        internal ExchangeBuilderWithOptions UseExchange(string exchange)
        {
            switch (exchange)
            {
                case ExchangeNames.Binance:
                    options.Exchange = exchange;
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

        public ExchangeBuilderWithOptions UseHttp(HttpClient httpClient)
        {
            options.Http = httpClient;
            return this;
        }

        public ExchangeBuilderWithOptions SetCredentials(string apiKey, string apiSecret)
        {
            options.ApiKey = apiKey;
            options.ApiSecret = apiSecret;
            return this;
        }

        public IExchange Build()
        {
            IExchange exchange;
            switch (options.Exchange)
            {
                case ExchangeNames.Binance:
                    exchange = new BinanceExchange(options);
                    break;
                case ExchangeNames.Bitfinex:
                case ExchangeNames.Bittrex:
                case ExchangeNames.Cobinhood:
                case ExchangeNames.Poloniex:
                default:
                    throw new NotImplementedException();
            }

            return exchange;
        }
    }

    public class ExchangeOptions
    {
        public string Exchange { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public HttpClient Http { get; set; }
    }
}
