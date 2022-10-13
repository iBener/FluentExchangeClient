using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceRequestTicker : BinanceBaseRequest
{
    private readonly string? _symbol;
    private readonly List<string>? _symbols;

    public BinanceRequestTicker(ExchangeOptions options) : this(String.Empty, String.Empty, options)
    {
    }

    public BinanceRequestTicker(string symbol, string quoteSymbol, ExchangeOptions options) : base(new { symbol = symbol + quoteSymbol }, options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/ticker/24hr" + QueryString);
        _symbol = $"{symbol}{quoteSymbol}";
    }

    public BinanceRequestTicker(List<string> symbols, ExchangeOptions options) : base(CreateParamsObject(symbols), options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/api/v3/ticker/24hr" + QueryString);
        _symbols = symbols;
    }

    private static object? CreateParamsObject(List<string> symbols)
    {
        if (symbols?.Count > 0)
        {
            string sym = $@"[""{String.Join(@""",""", symbols)}""]";
            return new { symbols = sym };
        }
        return null;
    }

    public override int Weight => 
        !String.IsNullOrEmpty(_symbol) ? 1 :
        _symbols?.Count switch
        {
            int i when i == 0 => 40,
            int i when i > 0 && i <= 20 => 1,
            int i when i > 20 && i <= 100 => 20,
            _ => 40
        };
}
