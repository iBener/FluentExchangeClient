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

    public void UseTestExchange()
    {
        Options.UseTestServer = true;
    }

    public IExchange BuildExchange()
    {
        return new BitfinexExchange(Options);
    }

    public IExchangeRaw BuildRawExchange()
    {
        //return new BitfinexExchange(Options);
        throw new NotImplementedException();
    }

    public IFuturesExchange BuildFuturesExchange()
    {
        throw new NotImplementedException();
    }

    public IFuturesExchangeRaw BuildRawFuturesExchange()
    {
        throw new NotImplementedException();
    }
}
