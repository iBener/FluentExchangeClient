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

    public DerivativeExchangeBuilder UseDerivativeExchange()
    {
        return new DerivativeExchangeBuilder(builder);
    }

    public DerivativeRawExchangeBuilder UseRawDerivativeExchange()
    {
        return new DerivativeRawExchangeBuilder(builder);
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

    public IExchange Build()
    {
        return builder.BuildExchange();
    }
}

public class DerivativeExchangeBuilder
{
    private readonly IExchangeBuilder builder;

    internal DerivativeExchangeBuilder(IExchangeBuilder builder)
    {
        this.builder = builder;
    }

    public IDerivativeExchange Build()
    {
        return builder.BuildDerivativeExchange();
    }
}

public class DerivativeRawExchangeBuilder
{
    private readonly IExchangeBuilder builder;

    internal DerivativeRawExchangeBuilder(IExchangeBuilder builder)
    {
        this.builder = builder;
    }

    public IDerivativeExchangeRaw Build()
    {
        return builder.BuildRawDerivativeExchange();
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
