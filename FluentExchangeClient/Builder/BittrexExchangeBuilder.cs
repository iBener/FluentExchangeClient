using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Bittrex;
using FluentExchangeClient.Mapper;
using System;

namespace FluentExchangeClient.Builder
{
    class BittrexExchangeBuilder : IExchangeBuilder
    {
        public ExchangeOptions Options { get; }

        public BittrexExchangeBuilder()
        {
            Options = new ExchangeOptions
            {
                ExchangeName = ExchangeNames.Bittrex,
                Mapper = MappingConfigurations.Bittrex.CreateMapper()
            };
        }

        public void SetCredentials(string apiKey, string apiSecret)
        {
            throw new NotImplementedException();
        }

        public IExchange Build()
        {
            //return new BittrexExchange(Options);
            throw new NotImplementedException();
        }

        public IExchangeRaw BuildRaw()
        {
            //return new BittrexExchange(Options);
            throw new NotImplementedException();
        }
    }
}
