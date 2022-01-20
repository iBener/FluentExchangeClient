using FluentExchangeClient.Exchange;

namespace FluentExchangeClient.Builder;

interface IExchangeBuilder
{
    ExchangeOptions Options { get; }
    void SetCredentials(string apiKey, string apiSecret);
    IExchange BuildExchange();
    IDerivativeExchange BuildDerivativeExchange();
    IExchangeRaw BuildRawExchange();
    IDerivativeExchangeRaw BuildRawDerivativeExchange();
}
