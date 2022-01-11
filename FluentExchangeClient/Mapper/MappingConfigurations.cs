using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Mapper;

static class MappingConfigurations
{
    /// Binance mapper configuration singleton object
    public static MapperConfiguration Binance => binanceLazyInstance.Value;

    private static readonly Lazy<MapperConfiguration> binanceLazyInstance =
        new(() => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BinanceMappingProfile());
        }));



    /// Bitfinex mapper configuration singleton object
    public static MapperConfiguration Bitfinex => bitfinexLazyInstance.Value;

    private static readonly Lazy<MapperConfiguration> bitfinexLazyInstance =
        new(() => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BitfinexMappingProfile());
        }));



    /// Bittrex mapper configuration singleton object
    public static MapperConfiguration Bittrex => bittrexLazyInstance.Value;

    private static readonly Lazy<MapperConfiguration> bittrexLazyInstance =
        new(() => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BittrexMappingProfile());
        }));



    /// Cobinhood mapper configuration singleton object
    public static MapperConfiguration Cobinhood => cobinhoodLazyInstance.Value;

    private static readonly Lazy<MapperConfiguration> cobinhoodLazyInstance =
        new(() => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CobinhoodMappingProfile());
        }));



    /// Poloniex mapper configuration singleton object
    public static MapperConfiguration Poloniex => poloniexLazyInstance.Value;

    private static readonly Lazy<MapperConfiguration> poloniexLazyInstance =
        new(() => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new PoloniexMappingProfile());
        }));

}
