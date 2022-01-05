using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange;

namespace FluentExchangeClient;

public class ExchangeBuilder
{
    public static ExchangeBuilderWithOptions UseBinance()
    {
        return new ExchangeBuilderWithOptions(ExchangeNames.Binance);
    }

    public static ExchangeBuilderWithOptions UseBinancePerpetual()
    {
        return new ExchangeBuilderWithOptions(ExchangeNames.BinancePerpetual);
    }

    public static ExchangeBuilderWithOptions UseBitfinex()
    {
        return new ExchangeBuilderWithOptions(ExchangeNames.Bitfinex);
    }

    public static ExchangeBuilderWithOptions UseBittrex()
    {
        return new ExchangeBuilderWithOptions(ExchangeNames.Bittrex);
    }

    public static ExchangeBuilderWithOptions UsePoloniex()
    {
        return new ExchangeBuilderWithOptions(ExchangeNames.Poloniex);
    }

    public static ExchangeBuilderWithOptions UseCobinhood()
    {
        return new ExchangeBuilderWithOptions(ExchangeNames.Cobinhood);
    }

    public static ExchangeBuilderWithOptions UseExchange(string exchange)
    {
        return new ExchangeBuilderWithOptions(exchange);
    }
}
