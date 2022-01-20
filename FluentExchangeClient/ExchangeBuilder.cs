using FluentExchangeClient.Builder;
using FluentExchangeClient.Builder.Exchange;
using FluentExchangeClient.Exchange;
using System;

namespace FluentExchangeClient;

public class ExchangeBuilder
{
    public static ExchangeBuilderWithOptions UseBinance()
    {
        var builder = new BinanceExchangeBuilder();
        return new ExchangeBuilderWithOptions(builder);
    }

    public static ExchangeBuilderWithOptions UseBitfinex()
    {
        var builder = new BitfinexExchangeBuilder();
        return new ExchangeBuilderWithOptions(builder);
    }

    public static ExchangeBuilderWithOptions UseBittrex()
    {
        var builder = new BittrexExchangeBuilder();
        return new ExchangeBuilderWithOptions(builder);
    }

    public static ExchangeBuilderWithOptions UsePoloniex()
    {
        var builder = new CobinhoodExchangeBuilder();
        return new ExchangeBuilderWithOptions(builder);
    }

    public static ExchangeBuilderWithOptions UseCobinhood()
    {
        var builder = new PoloniexExchangeBuilder();
        return new ExchangeBuilderWithOptions(builder);
    }

    public static ExchangeBuilderWithOptions UseExchange(string exchange)
    {
        IExchangeBuilder builder = exchange switch
        {
            ExchangeNames.Binance => new BinanceExchangeBuilder(),
            ExchangeNames.Bitfinex => new BitfinexExchangeBuilder(),
            ExchangeNames.Bittrex => new BittrexExchangeBuilder(),
            ExchangeNames.Cobinhood => new CobinhoodExchangeBuilder(),
            ExchangeNames.Poloniex => new PoloniexExchangeBuilder(),
            _ => throw new ExchangeClientException($"\"{exchange}\" is not supported. Supported exchanges:\n{ String.Join(", \n", ExchangeNames.List) }"),
        };
        return new ExchangeBuilderWithOptions(builder);
    }
}
