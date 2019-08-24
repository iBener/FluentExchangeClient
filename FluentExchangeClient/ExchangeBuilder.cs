using FluentExchangeClient.Models;
using System;
using System.Net.Http;

namespace FluentExchangeClient
{
    public class ExchangeBuilder
    {
        ExchangeBuilderWithOptions builder;
        ExchangeBuilderWithOptions Builder => builder??new ExchangeBuilderWithOptions();

        public ExchangeBuilderWithOptions UseBinance()
        {
            return Builder.UseExchange(ExchangeNames.Binance);
        }

        public ExchangeBuilderWithOptions UseBitfinex()
        {
            return Builder.UseExchange(ExchangeNames.Bitfinex);
        }

        public ExchangeBuilderWithOptions UseBittrex()
        {
            return Builder.UseExchange(ExchangeNames.Bittrex);
        }

        public ExchangeBuilderWithOptions UsePoloniex()
        {
            return Builder.UseExchange(ExchangeNames.Poloniex);
        }

        public ExchangeBuilderWithOptions UseCobinhood()
        {
            return Builder.UseExchange(ExchangeNames.Cobinhood);
        }

        public ExchangeBuilderWithOptions UseExchange(string exchange)
        {
            return Builder.UseExchange(exchange);
        }
    }

    public class ExchangeBuilderWithOptions
    {
        ExchangeOptions options;

        internal ExchangeBuilderWithOptions()
        {
            options = new ExchangeOptions();
        }

        internal ExchangeBuilderWithOptions UseExchange(string exchange)
        {
            options.Exchange = exchange;
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
            throw new NotImplementedException();
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
