#pragma warning disable CS0649
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System;
using System.Collections.Generic;

namespace FluentExchangeClient.Exchange.Binance.Responses;

class BinanceResponseExchangeInfo
{
    public List<BinanceResponseExchangeInfoSymbolInfo> symbols = new();
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
    public decimal minPrice;
    public decimal maxPrice;
    public decimal tickSize;
    public decimal multiplierUp;
    public decimal multiplierDown;
    public decimal avgPriceMins;
    public decimal minQty;
    public decimal maxQty;
    public decimal stepSize;
    public decimal minNotional;
    public bool applyToMarket;
    public int limit;
    public int maxNumOrders;
    public int maxNumAlgoOrders;
}
