#pragma warning disable CS0649
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinanceResponseTicker
{
    public string symbol;
    public decimal lastPrice;
    public decimal quoteVolume;
}
