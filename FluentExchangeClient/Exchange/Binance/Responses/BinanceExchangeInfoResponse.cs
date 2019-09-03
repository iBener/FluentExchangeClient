#pragma warning disable CS0649

using System.Collections.Generic;

namespace FluentExchangeClient.Exchange.Binance.Responses
{
    class BinanceExchangeInfoResponse
    {
        public List<BinanceSymbolInfo> symbols;
    }

    class BinanceSymbolInfo
    {
        public string symbol;
        public string baseAsset;
        public string quoteAsset;
        public List<BinanceFilterInfo> filters;
    }

    class BinanceFilterInfo
    {
        public string filterType;
        public decimal stepSize;
        public decimal tickSize;
    }
}
