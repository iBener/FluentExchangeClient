using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Bittrex;
using FluentExchangeClient.Mapper;
using System;

namespace FluentExchangeClient.Builder.Exchange;

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

    public IExchange BuildExchange()
    {
        //return new BittrexExchange(Options);
        throw new NotImplementedException();
    }

    public IExchange BuildPerpetualExchange()
    {
        throw new NotImplementedException();
    }

    public IExchangeRaw BuildRawExchange()
    {
        //return new BittrexExchange(Options);
        throw new NotImplementedException();
    }

    public IExchange BuildDerivativeExchange()
    {
        throw new NotImplementedException();
    }

    public IExchangeRaw BuildRawDerivativeExchange()
    {
        throw new NotImplementedException();
    }
}
