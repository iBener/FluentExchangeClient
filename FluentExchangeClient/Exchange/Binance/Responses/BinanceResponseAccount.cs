#pragma warning disable CS0649

using System.Collections.Generic;

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinanceResponseAccount
{
    public List<BinanceResponseAccountBalanceInfo> balances;
}

class BinanceResponseAccountBalanceInfo
{
    public string asset;
    public decimal free;
    public decimal locked;
}
