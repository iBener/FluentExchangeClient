using AutoMapper;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FluentExchangeClient.Mapper
{
    class BinanceMappingProfile : Profile
    {
        public BinanceMappingProfile()
        {
            CreateMap<BinanceTickerResponse, Ticker>()
                .ForMember(target => target.Pair, m => m.MapFrom(source => source.symbol))
                .ForMember(target => target.Price, m => m.MapFrom(source => source.price));
            CreateMap<BinanceBalanceInfo, Balance>()
                .ForMember(target => target.Symbol, m => m.MapFrom(source => source.asset))
                .ForMember(target => target.Amount, m => m.MapFrom(source => source.free + source.locked));
            CreateMap<BinanceSymbolInfo, Market>()
                .ConvertUsing<BinanceSymbolInfoResolver>();
            CreateMap<BinanceCandleResponse, Candle>();
            CreateMap<BinanceOrderResponse, Order>()
                .ForMember(target => target.Quantity, m => m.MapFrom(source => source.origQty))
                .ForMember(target => target.FilledQuantity, m => m.MapFrom(source => source.executedQty))
                .ForMember(target => target.TransactionTime, m => m.MapFrom(source => source.time.DateTime));
            CreateMap<BinanceTradeResponse, Trade>()
                .ForMember(target => target.Quantity, m => m.MapFrom(source => source.qty))
                .ForMember(target => target.QuoteQuantity, m => m.MapFrom(source => source.quoteQty))
                .ForMember(target => target.Time, m => m.MapFrom(source => source.time.DateTime))
                .ForMember(target => target.Side, m => m.MapFrom(source => source.isBuyer ? "BUY" : "SELL"));
        }
    }

    class BinanceSymbolInfoResolver : ITypeConverter<BinanceSymbolInfo, Market>
    {
        public Market Convert(BinanceSymbolInfo source, Market destination, ResolutionContext context)
        {
            var priceFilter = GetFilterValue(source, "PRICE_FILTER", x => x.tickSize);
            var lotSizeFilter = GetFilterValue(source, "LOT_SIZE", x => x.stepSize);

            return new Market
            {
                Base = source.baseAsset,
                Quote = source.quoteAsset,
                PriceTick = priceFilter.value,
                OrderTick = lotSizeFilter.value,
                PricePrecision = priceFilter.precision,
                OrderPrecision = lotSizeFilter.precision,
                Status = source.status
            };
        }

        (decimal value, int precision) GetFilterValue(BinanceSymbolInfo source, string filterType, Func<BinanceFilterInfo, decimal> func)
        {
            var filterInfo = source.filters.FirstOrDefault(f => f.filterType == filterType);
            var value = func(filterInfo);
            if (value >= 1)
            {
                return (value, 0);
            }
            string[] parts = value.ToString(CultureInfo.InvariantCulture).Split('.');
            int pointIndex = parts[1].IndexOf('1');
            var precision = parts[1].Substring(0, pointIndex < 0 ? 0 : pointIndex).Length + 1;
            return (value, precision);
        }
    }

    class BinanceSymbolInfoPrecisionResolver : IValueResolver<BinanceSymbolInfo, Market, int>
    {
        public int Resolve(BinanceSymbolInfo source, Market destination, int destMember, ResolutionContext context)
        {
            var tickSize = source.filters.FirstOrDefault(f => f.filterType == "PRICE_FILTER").tickSize;
            var parts = tickSize.ToString(CultureInfo.InvariantCulture).Split('.');
            var result = parts[1].Substring(0, parts[1].IndexOf('1')).Length + 1;

            return result;
        }
    }

    class BinanceSymbolInfoStepResolver : IValueResolver<BinanceSymbolInfo, Market, decimal>
    {
        public decimal Resolve(BinanceSymbolInfo source, Market destination, decimal destMember, ResolutionContext context)
        {
            var lotSize = source.filters.FirstOrDefault(f => f.filterType == "LOT_SIZE").stepSize;
            return lotSize;
        }
    }
}
