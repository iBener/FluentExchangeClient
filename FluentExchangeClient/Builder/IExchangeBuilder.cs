using FluentExchangeClient.Exchange;

namespace FluentExchangeClient.Builder
{
    public interface IExchangeBuilder
    {
        ExchangeOptions Options { get; }
        void SetCredentials(string apiKey, string apiSecret);
        IExchange Build();
    }
}
