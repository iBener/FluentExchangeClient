using FluentExchangeClient.Exchange;

namespace FluentExchangeClient.Builder;

interface IExchangeBuilder
{
    ExchangeOptions Options { get; }
    void SetCredentials(string apiKey, string apiSecret);
    IExchange BuildExchange();
    IFuturesExchange BuildFuturesExchange();
    IExchangeRaw BuildRawExchange();
    IFuturesExchangeRaw BuildRawFuturesExchange();
}
