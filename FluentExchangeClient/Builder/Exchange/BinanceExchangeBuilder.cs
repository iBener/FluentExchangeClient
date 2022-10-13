using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Binance;
using FluentExchangeClient.Mapper;
using System;
using System.Security.Cryptography;
using System.Text;

namespace FluentExchangeClient.Builder.Exchange;

class BinanceExchangeBuilder : IExchangeBuilder
{
    public ExchangeOptions Options { get; protected set; }

    public BinanceExchangeBuilder()
    {
        Options = new ExchangeOptions
        {
            ExchangeName = ExchangeNames.Binance,
            Mapper = MappingConfigurations.Binance.CreateMapper(),
        };
    }

    public void SetCredentials(string apiKey, string apiSecret)
    {
        byte[] privateKey = Encoding.UTF8.GetBytes(apiSecret);
        Options.Credentials = new ApiCredentials
        {
            ApiKey = apiKey,
            Hash = new HMACSHA256(privateKey),
        };
    }

    public void UseTestExchange()
    {
        Options.UseTestServer = true;
    }

    public IExchange BuildExchange()
    {
        return new BinanceExchange(Options);
    }

    public IFuturesExchange BuildFuturesExchange()
    {
        return new BinanceFuturesExchange(Options);
    }

    public IExchangeRaw BuildRawExchange()
    {
        return new BinanceExchangeRaw(Options);
    }

    public IFuturesExchangeRaw BuildRawFuturesExchange()
    {
        return new BinanceFuturesExchangeRaw(Options);
    }
}
