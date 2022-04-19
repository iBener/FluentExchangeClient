﻿using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

public class BinanceExchange : BinanceExchangeRaw, IExchange
{
    public BinanceExchange(ExchangeOptions options) : base(options)
    {
    }

    public new async Task<Market?> GetMarketAsync(string symbol, string quoteSymbol)
    {
        var markets = await GetMarketsAsync();
        return markets.FirstOrDefault(x => x.Base == symbol && x.Quote == quoteSymbol);
    }

    public new async Task<IEnumerable<Market>> GetMarketsAsync()
    {
        var request = new BinanceRequestExchangeInfo();
        var markets = await SendAsync<BinanceResponseExchangeInfo>(request);
        return Map<IEnumerable<Market>>(markets.symbols);
    }

    public new async Task<Ticker?> GetTickerAsync(string symbol, string quoteSymbol)
    {
        var request = new BinanceRequestTicker(symbol, quoteSymbol);
        var response = await SendAsync<BinanceResponseTicker>(request);
        return Map<Ticker>(response);
    }

    public new async Task<IEnumerable<Ticker>> GetTickersAsync()
    {
        var request = new BinanceRequestTicker();
        var tickers = await SendAsync<List<BinanceResponseTicker>>(request);
        return Map<IEnumerable<Ticker>>(tickers);
    }

    public new async Task<IEnumerable<Balance>> GetBalancesAsync()
    {
        var request = new BinanceRequestBalance(Timestamp, Options.Credentials);
        var account = await SendAsync<BinanceResponseAccount>(request);
        return Map<IEnumerable<Balance>>(account.balances.Where(x => x.free + x.locked > 0));
    }

    public new async Task<Balance?> GetBalanceAsync(string symbol)
    {
        var balances = await GetBalancesAsync();
        return balances?.FirstOrDefault(x => x.Symbol == symbol);
    }

    public new async Task<DateTimeOffset> GetServerTime()
    {
        var request = new BinanceRequestServerTime();
        var response = await SendAsync<BinanceResponseServerTime>(request);
        return response.serverTime; //DateTimeOffset.FromUnixTimeMilliseconds(response.serverTime);
    }

    public new async Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
    {
        var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
        return await GetCandlesAsyncInternal(symbol, quoteSymbol, request);
    }

    private async Task<IEnumerable<Candle>> GetCandlesAsyncInternal(string symbol, string quoteSymbol, BinanceBaseRequest request)
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

    public new async Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 500)
    {
        var marketsJson = await base.GetMarketsAsync();
        var markets = JObject.Parse(marketsJson).SelectTokens($"$.symbols[?(@.quoteAsset == '{quoteSymbol}')]");
        var result = new Dictionary<string, IEnumerable<Candle>>();
        foreach (var market in markets)
        {
            string? symbol = market?["baseAsset"]?.Value<string>();
            if (!String.IsNullOrEmpty(symbol))
            {
                var candles = await GetCandlesAsync(symbol, quoteSymbol, interval, limit);
                result[symbol + quoteSymbol] = candles;
            }
        }
        return result;
    }

    public new async Task<Order?> GetOrder(string symbol, string? orderId = null, string? clientOrderId = null)
    {
        var param = new
        {
            symbol,
            orderId,
            clientOrderId,
            timestamp = Timestamp
        };
        var request = new BinanceRequestGetOrder(param, Options.Credentials);
        var order = await SendAsync<BinanceResponseOrder>(request);
        return Map<Order>(order);
    }

    public new Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, int limit)
    {
        return GetOrders(symbol, quoteSymbol, default, default, limit);
    }

    public new async Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit)
    {
        limit = Math.Clamp(limit, 1, 1000);
        var request = new BinanceRequestOrders(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
        var orders = await SendAsync<IEnumerable<BinanceResponseOrder>>(request);
        return Map<IEnumerable<Order>>(orders);
    }

    public new Task<IEnumerable<Order>> GetOpenOrders()
    {
        return GetOpenOrders(String.Empty, String.Empty);
    }

    public new async Task<IEnumerable<Order>> GetOpenOrders(string symbol, string quoteSymbol)
    {
        var request = new BinanceRequestOpenOrders(symbol, quoteSymbol, Timestamp, Options.Credentials);
        var orders = await SendAsync<IEnumerable<BinanceResponseOrder>>(request);
        return Map<IEnumerable<Order>>(orders);
    }

    public new Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, int limit = 500)
    {
        return GetTrades(symbol, quoteSymbol, default, default, limit);
    }

    public new async Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
    {
        var tradesRaw = await base.GetTrades(symbol, quoteSymbol, start, end, limit);
        var trades = JsonConvert.DeserializeObject<IEnumerable<BinanceResponseTrade>>(tradesRaw);
        return GroupTrades(trades);
    }

    public new async Task<Order?> PostOrder(Order order, bool test = false)
    {
        var newOrderJson = await base.PostOrder(order, test);
        var newOrder = JsonConvert.DeserializeObject<BinanceResponseOrder>(newOrderJson);
        return newOrder != null ? Map<Order>(newOrder) : null;
    }

    public new async Task<Order?> DeleteOrder(Order order)
    {
        var canceledOrderJson = await base.DeleteOrder(order);
        var canceledOrder = JsonConvert.DeserializeObject<BinanceResponseOrderDelete>(canceledOrderJson);
        return canceledOrder != null ? Map<Order>(canceledOrder) : null;
    }
}
