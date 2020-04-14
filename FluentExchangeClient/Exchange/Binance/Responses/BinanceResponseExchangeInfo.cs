#pragma warning disable CS0649

using System.Collections.Generic;

namespace FluentExchangeClient.Exchange.Binance.Responses
{
    class BinanceResponseExchangeInfo
    {
        public List<BinanceResponseExchangeInfoSymbolInfo> symbols;
    }

    class BinanceResponseExchangeInfoSymbolInfo
    {
        public string symbol;
        public string baseAsset;
        public string quoteAsset;
        public List<BinanceResponseExchangeInfoSymbolInfoFilterInfo> filters;
        public string status;
    }

    class BinanceResponseExchangeInfoSymbolInfoFilterInfo
    {
        public string filterType;
        public decimal stepSize;
        public decimal tickSize;
    }
}
