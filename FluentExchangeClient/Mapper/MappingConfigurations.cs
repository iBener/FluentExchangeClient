using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Mapper
{
    static class MappingConfigurations
    {
        private static readonly Lazy<MapperConfiguration> binanceLazy =
            new Lazy<MapperConfiguration>(() => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BinanceMappingProfile());
            }));

        public static MapperConfiguration Binance { get { return binanceLazy.Value; } }

    }
}
