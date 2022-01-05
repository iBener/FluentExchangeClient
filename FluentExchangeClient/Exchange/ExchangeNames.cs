using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Exchange;

public static class ExchangeNames
{
    public const string Binance = "Binance";

    public const string BinancePerpetual = "BinancePerpetual";

    public const string Bitfinex = "Bitfinex";

    public const string Bittrex = "Bittrex";

    public const string Cobinhood = "Cobinhood";

    public const string Poloniex = "Poloniex";

    public static string[] List => new[]
    {
        Binance,
        Bitfinex,
        Bittrex,
        Cobinhood,
        Poloniex
    };
}
