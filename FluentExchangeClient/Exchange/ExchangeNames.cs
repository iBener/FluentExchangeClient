using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Exchange;

public static class ExchangeNames
{
    public const string Binance = "Binance";

    public const string Bitfinex = "Bitfinex";

    public const string Bittrex = "Bittrex";

    public const string Cobinhood = "Cobinhood";

    public const string Poloniex = "Poloniex";

    public const string FTX = "FTX";

    public const string Kraken = "Kraken";

    public const string KuCoin = "KuCoin";

    public static string[] List => new[]
    {
        Binance,
        Bitfinex,
        Bittrex,
        Cobinhood,
        Poloniex,
        FTX,
        Kraken,
        KuCoin,
    };
}
