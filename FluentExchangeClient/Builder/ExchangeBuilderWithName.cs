using FluentExchangeClient.Exchange;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentExchangeClient.Builder
{
    public class ExchangeBuilderWithName
    {
        IExchangeBuilder builder;

        public ExchangeBuilderWithName(string exchangeName)
        {
            UseExchange(exchangeName);
        }

        public ExchangeBuilderWithName(Account account)
        {
            UseExchange(account.Exchange);
            SetCredentials(account.ApiKey, account.ApiSecret);
        }

        internal ExchangeBuilderWithName UseExchange(string exchange)
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
