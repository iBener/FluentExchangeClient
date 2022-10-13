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

    public void UseTestExchange()
    {
        Options.UseTestServer = true;
    }

    public IExchange BuildExchange()
    {
        //return new BittrexExchange(Options);
        throw new NotImplementedException();
    }

    public IExchangeRaw BuildRawExchange()
    {
        //return new BittrexExchange(Options);
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
