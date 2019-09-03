using AutoMapper;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
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
                .ForMember(target => target.Base, m => m.MapFrom(source => source.baseAsset))
                .ForMember(target => target.Quote, m => m.MapFrom(source => source.quoteAsset));
            //.ForMember(target => target.Precision, m => m.MapFrom(source => source.filters));
            CreateMap<BinanceCandleResponse, Candle>();
        }
    }
}
