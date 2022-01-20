using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Bitfinex;
using FluentExchangeClient.Mapper;
using System;

namespace FluentExchangeClient.Builder.Exchange;

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

    public IExchange BuildExchange()
    {
        return new BitfinexExchange(Options);
    }

    public IExchange BuildPerpetualExchange()
    {
        throw new NotImplementedException();
    }

    public IExchangeRaw BuildRawExchange()
    {
        //return new BitfinexExchange(Options);
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
