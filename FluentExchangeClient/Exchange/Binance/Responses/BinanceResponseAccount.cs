#pragma warning disable CS0649
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Collections.Generic;

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinanceResponseAccount
{
    public List<BinanceResponseAccountBalanceInfo> balances = new();
}

class BinanceResponseAccountBalanceInfo
{
    public string asset;
    public decimal free;
    public decimal locked;
}
