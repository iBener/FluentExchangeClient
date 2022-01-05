using FluentExchangeClient.Exchange;
using FluentExchangeClient.Exchange.Binance;
using FluentExchangeClient.Mapper;
using System.Security.Cryptography;
using System.Text;

namespace FluentExchangeClient.Builder;

class BinanceExchangeBuilder : IExchangeBuilder
{
    private readonly bool _usePerpetual;

    public ExchangeOptions Options { get; }

    public BinanceExchangeBuilder(bool usePerpetual = false)
    {
        Options = new ExchangeOptions
        {
            ExchangeName = ExchangeNames.Binance,
            Mapper = MappingConfigurations.Binance.CreateMapper(),
        };
        _usePerpetual = usePerpetual;
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

    public IExchange Build()
    {
        if (_usePerpetual)
        {
            return new BinancePerpetualExchange(Options);
        }
        return new BinanceExchange(Options);
    }

    public IExchangeRaw BuildRaw()
    {
        return new BinanceExchangeRaw(Options);
    }
}
