using FluentExchangeClient.Exchange;

namespace FluentExchangeClient.Builder;

interface IExchangeBuilder
{
    ExchangeOptions Options { get; }
    void SetCredentials(string apiKey, string apiSecret);
    IExchange BuildExchange();
    IExchange BuildDerivativeExchange();
    IExchangeRaw BuildRawExchange();
    IExchangeRaw BuildRawDerivativeExchange();
}
