using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange;
using FluentExchangeClient.Models;

namespace FluentExchangeClient
{
    public class ExchangeBuilder
    {
        public ExchangeBuilderWithOptions UseBinance()
        {
            return new ExchangeBuilderWithOptions(ExchangeNames.Binance);
        }

        public ExchangeBuilderWithOptions UseBitfinex()
        {
            return new ExchangeBuilderWithOptions(ExchangeNames.Bitfinex);
        }

        public ExchangeBuilderWithOptions UseBittrex()
        {
            return new ExchangeBuilderWithOptions(ExchangeNames.Bittrex);
        }

        public ExchangeBuilderWithOptions UsePoloniex()
        {
            return new ExchangeBuilderWithOptions(ExchangeNames.Poloniex);
        }

        public ExchangeBuilderWithOptions UseCobinhood()
        {
            return new ExchangeBuilderWithOptions(ExchangeNames.Cobinhood);
        }

        public ExchangeBuilderWithOptions UseExchange(string exchange)
        {
            return new ExchangeBuilderWithOptions(exchange);
        }
    }
}
