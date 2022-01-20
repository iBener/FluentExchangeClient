using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Poloniex;
using FluentExchangeClient.Mapper;
using System;

namespace FluentExchangeClient.Builder.Exchange;

class PoloniexExchangeBuilder : IExchangeBuilder
{
    public ExchangeOptions Options { get; }

    public PoloniexExchangeBuilder()
    {
        Options = new ExchangeOptions
        {
            ExchangeName = ExchangeNames.Poloniex,
            Mapper = MappingConfigurations.Poloniex.CreateMapper()
        };
    }

    public void SetCredentials(string apiKey, string apiSecret)
    {
        throw new NotImplementedException();
    }

    public IExchange BuildExchange()
    {
        //return new PoloniexExchange(Options);
        throw new NotImplementedException();
    }

    public IExchangeRaw BuildRawExchange()
    {
        //return new PoloniexExchange(Options);
        throw new NotImplementedException();
    }

    public IDerivativeExchange BuildDerivativeExchange()
    {
        throw new NotImplementedException();
    }

    public IDerivativeExchangeRaw BuildRawDerivativeExchange()
    {
        throw new NotImplementedException();
    }
}
