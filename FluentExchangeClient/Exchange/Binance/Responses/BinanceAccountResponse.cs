#pragma warning disable CS0649

using System.Collections.Generic;

namespace FluentExchangeClient.Exchange.Binance.Responses
{
    class BinanceAccountResponse
    {
        public List<BinanceBalanceInfo> balances;
    }

    class BinanceBalanceInfo
    {
        public string asset;
        public decimal free;
        public decimal locked;
    }
}
