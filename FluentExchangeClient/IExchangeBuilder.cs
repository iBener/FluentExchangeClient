using FluentExchangeClient.Exchange;
using FluentExchangeClient.Internal;

namespace FluentExchangeClient
{
    internal interface IExchangeBuilder
    {
        ExchangeOptions Options { get; }
        void SetCredentials(string apiKey, string apiSecret);
        IExchange Build();
    }
}
