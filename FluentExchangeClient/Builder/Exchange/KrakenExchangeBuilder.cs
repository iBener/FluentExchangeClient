using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Kraken;
using FluentExchangeClient.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Builder.Exchange;

class KrakenExchangeBuilder : IExchangeBuilder
{
    public ExchangeOptions Options { get; }

    public KrakenExchangeBuilder()
    {
        Options = new ExchangeOptions
        {
            ExchangeName = ExchangeNames.FTX,
            Mapper = MappingConfigurations.Cobinhood.CreateMapper()
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
        //return new KrakenExchange(Options);
        throw new NotImplementedException();
    }

    public IExchangeRaw BuildRawExchange()
    {
        //return new KrakenExchangeRaw(Options);
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
