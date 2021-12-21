#pragma warning disable CS0649

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinanceResponseTicker
{
    public string symbol;
    public decimal lastPrice;
    public decimal quoteVolume;
}
