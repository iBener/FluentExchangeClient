using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange;
using FluentExchangeClient.Models;

namespace FluentExchangeClient
{
    public class ExchangeBuilder
    {
        public ExchangeBuilderWithName UseBinance()
        {
            return new ExchangeBuilderWithName(ExchangeNames.Binance);
        }

        public ExchangeBuilderWithName UseBitfinex()
        {
            return new ExchangeBuilderWithName(ExchangeNames.Bitfinex);
        }

        public ExchangeBuilderWithName UseBittrex()
        {
            return new ExchangeBuilderWithName(ExchangeNames.Bittrex);
        }

        public ExchangeBuilderWithName UsePoloniex()
        {
            return new ExchangeBuilderWithName(ExchangeNames.Poloniex);
        }

        public ExchangeBuilderWithName UseCobinhood()
        {
            return new ExchangeBuilderWithName(ExchangeNames.Cobinhood);
        }

        public ExchangeBuilderWithName UseExchange(string exchange)
        {
            return new ExchangeBuilderWithName(exchange);
        }

        public ExchangeBuilderWithName UseAccount(Account account)
        {
            return new ExchangeBuilderWithName(account);
        }
    }
}
