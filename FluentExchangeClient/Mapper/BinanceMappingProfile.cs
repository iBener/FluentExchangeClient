using AutoMapper;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FluentExchangeClient.Mapper;

class BinanceMappingProfile : Profile
{
    public BinanceMappingProfile()
    {
        // BinanceResponseTicker -> Ticker
        CreateMap<BinanceResponseTicker, Ticker>()
            .ForMember(target => target.Pair, m => m.MapFrom(source => source.symbol))
            .ForMember(target => target.Price, m => m.MapFrom(source => source.lastPrice))
            .ForMember(target => target.Volume, m => m.MapFrom(source => source.quoteVolume));

        // BinanceResponseAccountBalanceInfo -> Balance
        CreateMap<BinanceResponseAccountBalanceInfo, Balance>()
            .ForMember(target => target.Symbol, m => m.MapFrom(source => source.asset))
            .ForMember(target => target.Amount, m => m.MapFrom(source => source.free + source.locked));

        // BinancePerpetualResponseAccountAssets -> Balance
        CreateMap<BinanceFuturesResponseAccountAssets, Balance>()
            .ForMember(target => target.Symbol, m => m.MapFrom(source => source.asset))
            .ForMember(target => target.Amount, m => m.MapFrom(source => source.walletBalance))
            .ForMember(target => target.Locked, m => m.MapFrom(source => source.walletBalance - source.availableBalance))
            .ForMember(target => target.Free, m => m.MapFrom(source => source.availableBalance));

        // BinanceResponseExchangeInfoSymbolInfo -> Market
        CreateMap<BinanceResponseExchangeInfoSymbolInfo, Market>()
            .ConvertUsing<BinanceSymbolInfoResolver>();

        // BinanceCandleResponse -> Candle
        CreateMap<BinanceCandleResponse, Candle>();

        // BinanceResponseOrder -> Order
        CreateMap<BinanceResponseOrder, Order>()
            .ForMember(target => target.Quantity, m => m.MapFrom(source => source.origQty))
            .ForMember(target => target.Price, m => m.MapFrom(source => source.avgPrice))
            .ForMember(target => target.QuoteQuantity, m => m.MapFrom(source => source.cumQuote))
            .ForMember(target => target.FilledQuantity, m => m.MapFrom(source => source.executedQty))
            .ForMember(target => target.TransactionTime, m => m.MapFrom(source => source.time.DateTime));

        // BinanceResponseTrade -> Trade
        CreateMap<BinanceResponseTrade, Trade>()
            .ForMember(target => target.Quantity, m => m.MapFrom(source => source.qty))
            .ForMember(target => target.QuoteQuantity, m => m.MapFrom(source => source.quoteQty))
            .ForMember(target => target.Time, m => m.MapFrom(source => source.time.DateTime))
            .ForMember(target => target.Side, m => m.MapFrom(source => 
                !string.IsNullOrEmpty(source.side) ? 
                    source.side : source.isBuyer ? "BUY" : "SELL"));

        // BinanceResponseOrderDelete -> Order
        CreateMap<BinanceResponseOrderDelete, Order>()
            .ForMember(target => target.FilledQuantity, m => m.MapFrom(source => source.executedQty))
            .ForMember(target => target.Quantity, m => m.MapFrom(source => source.origQty))
            .ForMember(target => target.QuoteQuantity, m => m.MapFrom(source => source.cummulativeQuoteQty))
            .ForMember(target => target.ClientOrderId, m => m.MapFrom(source => source.origClientOrderId));

        // BinanceResponseLeverage -> Leverage
        CreateMap<BinanceResponseLeverage, Leverage>()
            .ForMember(target => target.InitialLeverage, m => m.MapFrom(source => source.leverage));

        // BinanceResponseObject -> Response
        CreateMap<BinanceResponseObject, Response>()
            .ForMember(target => target.Message, m => m.MapFrom(source => source.msg));
    }
}

class BinanceSymbolInfoResolver : ITypeConverter<BinanceResponseExchangeInfoSymbolInfo, Market>
{
    public Market Convert(BinanceResponseExchangeInfoSymbolInfo source, Market destination, ResolutionContext context)
    {
        var (priceValue, pricePrecision) = GetFilterValue(source, "PRICE_FILTER", x => x?.tickSize ?? 0);
        var (orderValue, orderPrecision) = GetFilterValue(source, "LOT_SIZE", x => x?.stepSize ?? 0);

        return new Market
        {
            Base = source.baseAsset,
            Quote = source.quoteAsset,
            PriceTick = priceValue,
            OrderTick = orderValue,
            PricePrecision = pricePrecision,
            OrderPrecision = orderPrecision,
            Status = source.status
        };
    }

    static (decimal value, int precision) GetFilterValue(BinanceResponseExchangeInfoSymbolInfo source, string filterType, Func<BinanceResponseExchangeInfoSymbolInfoFilterInfo?, decimal> func)
    {
        var filterInfo = source.filters?.FirstOrDefault(f => f.filterType == filterType);
        var value = func(filterInfo);
        if (value >= 1)
        {
            return (value, 0);
        }
        string[] parts = value.ToString(CultureInfo.InvariantCulture).Split('.');
        int pointIndex = parts[1].IndexOf('1');
        var precision = parts[1][..(pointIndex < 0 ? 0 : pointIndex)].Length + 1;
        return (value, precision);
    }
}

class BinanceSymbolInfoPrecisionResolver : IValueResolver<BinanceResponseExchangeInfoSymbolInfo, Market, int>
{
    public int Resolve(BinanceResponseExchangeInfoSymbolInfo source, Market destination, int destMember, ResolutionContext context)
    {
        var tickSize = source.filters.FirstOrDefault(f => f.filterType == "PRICE_FILTER")?.tickSize;
        var parts = tickSize?.ToString(CultureInfo.InvariantCulture).Split('.');
        var result = parts?[1][..parts[1].IndexOf('1')].Length + 1;

        return result ?? 0;
    }
}

class BinanceSymbolInfoStepResolver : IValueResolver<BinanceResponseExchangeInfoSymbolInfo, Market, decimal>
{
    public decimal Resolve(BinanceResponseExchangeInfoSymbolInfo source, Market destination, decimal destMember, ResolutionContext context)
    {
        var lotSize = source.filters.FirstOrDefault(f => f.filterType == "LOT_SIZE")?.stepSize;
        return lotSize ?? 0;
    }
}
