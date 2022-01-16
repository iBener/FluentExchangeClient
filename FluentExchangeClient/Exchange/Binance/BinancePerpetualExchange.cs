﻿using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

public class BinancePerpetualExchange : BinancePerpetualExchangeRaw, IExchange
{
    public BinancePerpetualExchange(ExchangeOptions options) : base(options)
    {
    }

    public new Task DeleteOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public new Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new async Task<Balance> GetBalanceAsync(string symbol)
    {
        var balances = await GetPerpetualBalancesAsync();
        return balances.FirstOrDefault(x => x.Symbol == symbol);
    }

    public new async Task<IEnumerable<Balance>> GetBalancesAsync()
    {
        var response = await base.GetBalancesAsync();
        var account = JsonConvert.DeserializeObject<BinancePerpetualResponseAccount>(response);
        return Map<IEnumerable<Balance>>(account.assets.Where(x => x.marginBalance > 0));
    }

    public new async Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
    {
        var request = new BinanceRequestPerpetualCandle(symbol, quoteSymbol, interval, limit);
        return await GetCandlesAsyncInternal(symbol, quoteSymbol, request);
    }

    private async Task<IEnumerable<Candle>> GetCandlesAsyncInternal(string symbol, string quoteSymbol, BinanceBasePerpetualRequest request)
    {
        var candles = await SendAsync<IEnumerable<BinanceCandleResponse>>(request);
        var result = Map<IEnumerable<Candle>>(candles);
        foreach (var candle in result)
        {
            candle.Base = symbol;
            candle.Quote = quoteSymbol;
        }
        return result;
    }

    public new async Task<Market> GetMarketAsync(string symbol, string quoteSymbol)
    {
        var markets = await GetMarketsAsync();
        return markets.FirstOrDefault(x => x.Base == symbol && x.Quote == quoteSymbol);
    }

    public new async Task<IEnumerable<Market>> GetMarketsAsync()
    {
        var response = await base.GetMarketsAsync();
        var markets = JsonConvert.DeserializeObject<BinanceResponseExchangeInfo>(response);
        return Map<IEnumerable<Market>>(markets.symbols);
    }

    public new Task<IEnumerable<Order>> GetOpenOrders()
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Order>> GetOpenOrders(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Balance>> GetPerpetualBalancesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Candle>> GetPerpetualCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new async Task<DateTimeOffset> GetServerTime()
    {
        var request = new BinancePerpetualRequestServerTime();
        var response = await SendAsync<BinanceResponseServerTime>(request);
        return response.serverTime;
    }

    public new async Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol)
    {
        var response = await base.GetTickerAsync(symbol, quoteSymbol);
        var ticker = JsonConvert.DeserializeObject<BinanceResponseTicker>(response);
        return Map<Ticker>(ticker);
    }

    public new Task<IEnumerable<Ticker>> GetTickersAsync()
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<Order> PostOrder(Order order, bool test = false)
    {
        throw new NotImplementedException();
    }
}
