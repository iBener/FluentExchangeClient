using FluentExchangeClient.Builder.Exchange;
using FluentExchangeClient.Exchange;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentExchangeClient.Builder;

public class ExchangeBuilderWithOptions
{
    private readonly IExchangeBuilder builder;

    internal ExchangeBuilderWithOptions(IExchangeBuilder builder)
    {
        this.builder = builder;
    }

    public FuturesExchangeBuilder UseFuturesExchange()
    {
        return new FuturesExchangeBuilder(builder);
    }

    public FuturesRawExchangeBuilder UseRawFuturesExchange()
    {
        return new FuturesRawExchangeBuilder(builder);
    }

    public RawExchangeBuilder UseRawExchange()
    {
        return new RawExchangeBuilder(builder);
    }

    public ExchangeBuilderWithOptions UseHttp(HttpClient httpClient)
    {
        builder.Options.Http = httpClient;
        return this;
    }

    public ExchangeBuilderWithOptions SetCredentials(string apiKey, string apiSecret)
    {
        builder.SetCredentials(apiKey, apiSecret);
        return this;
    }

    public ExchangeBuilderWithOptions UseNormalizedSymbols()
    {
        builder.Options.Symbols = SymbolDefinition.NormalizedSymbols;
        return this;
    }

    public ExchangeBuilderWithOptions UseExchangeSymbols()
    {
        builder.Options.Symbols = SymbolDefinition.ExchangeSymbols;
        return this;
    }

    public ExchangeBuilderWithOptions UseTestExchange()
    {
        builder.UseTestExchange();
        return this;
    }

    public IExchange Build()
    {
        return builder.BuildExchange();
    }
}

public class FuturesExchangeBuilder
{
    private readonly IExchangeBuilder builder;

    internal FuturesExchangeBuilder(IExchangeBuilder builder)
    {
        this.builder = builder;
    }

    public IFuturesExchange Build()
    {
        return builder.BuildFuturesExchange();
    }
}

public class FuturesRawExchangeBuilder
{
    private readonly IExchangeBuilder builder;

    internal FuturesRawExchangeBuilder(IExchangeBuilder builder)
    {
        this.builder = builder;
    }

    public IFuturesExchangeRaw Build()
    {
        return builder.BuildRawFuturesExchange();
    }
}

public class RawExchangeBuilder
{
    private readonly IExchangeBuilder builder;

    internal RawExchangeBuilder(IExchangeBuilder builder)
    {
        this.builder = builder;
    }

    public IExchangeRaw Build()
    {
        return builder.BuildRawExchange();
    }
}
