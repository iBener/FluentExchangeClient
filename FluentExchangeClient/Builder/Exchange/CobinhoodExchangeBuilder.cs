using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Cobinhood;
using FluentExchangeClient.Mapper;
using System;

namespace FluentExchangeClient.Builder.Exchange;

class CobinhoodExchangeBuilder : IExchangeBuilder
{
    public ExchangeOptions Options { get; }

    public CobinhoodExchangeBuilder()
    {
        Options = new ExchangeOptions
        {
            ExchangeName = ExchangeNames.Cobinhood,
            Mapper = MappingConfigurations.Cobinhood.CreateMapper()
        };
    }

    public void SetCredentials(string apiKey, string apiSecret)
    {
        throw new NotImplementedException();
    }

    public IExchange BuildExchange()
    {
        //return new CobinhoodExchange(Options);
        throw new NotImplementedException();
    }

    public IExchangeRaw BuildRawExchange()
    {
        //return new CobinhoodExchange(Options);
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
