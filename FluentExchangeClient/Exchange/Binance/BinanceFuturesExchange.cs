﻿using FluentExchangeClient.Builder;
using FluentExchangeClient.Common;
using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

public class BinanceFuturesExchange : BinanceFuturesExchangeRaw, IFuturesExchange
{
    public BinanceFuturesExchange(ExchangeOptions options) : base(options)
    {
    }

    public new async Task<Order?> DeleteOrder(Order order)
    {
        var canceledOrderJson = await base.DeleteOrder(order);
        var canceledOrder = JsonConvert.DeserializeObject<BinanceResponseOrderDelete>(canceledOrderJson);
        return Map<Order>(canceledOrder ?? new());
    }

    public new async Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
    {
        var marketsJson = await base.GetMarketsAsync();
        var markets = JObject.Parse(marketsJson).SelectTokens($"$.symbols[?(@.quoteAsset == '{quoteSymbol}')]");
        var result = new Dictionary<string, IEnumerable<Candle>>();
        if (markets != null)
        {
            foreach (var market in markets)
            {
                string? symbol = market?["baseAsset"]?.Value<string>();
                if (!String.IsNullOrEmpty(symbol))
                {
                    var candles = await GetCandlesAsync(symbol, quoteSymbol, interval, limit);
                    result[symbol + quoteSymbol] = candles;
                }
            }
        }
        return result;
    }

    public new async Task<Balance?> GetBalanceAsync(string symbol)
    {
        var balances = await GetPerpetualBalancesAsync();
        return balances.FirstOrDefault(x => x.Symbol == symbol);
    }

    public new async Task<IEnumerable<Balance>> GetBalancesAsync()
    {
        var request = new BinanceFuturesRequestBalance(Timestamp, Options.Credentials);
        var account = await SendAsync<BinanceFuturesResponseAccount>(request);
        return Map<IEnumerable<Balance>>(account.assets.Where(x => x.marginBalance > 0));
    }

    public new async Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
    {
        var request = new BinanceFuturesRequestCandle(symbol, quoteSymbol, interval, limit);
        return await GetCandlesAsyncInternal(symbol, quoteSymbol, request);
    }

    private async Task<IEnumerable<Candle>> GetCandlesAsyncInternal(string symbol, string quoteSymbol, BinanceBaseFuturesRequest request)
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

    public new async Task<Market?> GetMarketAsync(string symbol, string quoteSymbol)
    {
        var markets = await GetMarketsAsync();
        return markets.FirstOrDefault(x => x.Base == symbol && x.Quote == quoteSymbol);
    }

    public new async Task<IEnumerable<Market>> GetMarketsAsync()
    {
        var request = new BinanceFuturesRequestExchangeInfo();
        var markets = await SendAsync<BinanceResponseExchangeInfo>(request);
        return Map<IEnumerable<Market>>(markets.symbols);
    }

    public new Task<IEnumerable<Order>> GetOpenOrders()
    {
        return GetOpenOrders(String.Empty, String.Empty);
    }

    public new async Task<IEnumerable<Order>> GetOpenOrders(string symbol, string quoteSymbol)
    {
        var request = new BinanceFuturesRequestOpenOrders(symbol, quoteSymbol, Timestamp, Options.Credentials);
        var orders = await SendAsync<IEnumerable<BinanceResponseOrder>>(request);
        return Map<IEnumerable<Order>>(orders);
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
        var request = new BinanceFuturesRequestGetOrder(param, Options.Credentials);
        var order = await SendAsync<BinanceResponseOrder>(request);
        return Map<Order>(order);
    }

    public new Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetOrders(symbol, quoteSymbol, default, default, limit);
    }

    public new async Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        limit = Math.Clamp(limit, 1, 1000);
        var request = new BinanceFuturesRequestOrders(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
        var orders = await SendAsync<IEnumerable<BinanceResponseOrder>>(request);
        return Map<IEnumerable<Order>>(orders);
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
        var request = new BinanceFuturesRequestServerTime();
        var response = await SendAsync<BinanceResponseServerTime>(request);
        return response.serverTime;
    }

    public new async Task<Ticker?> GetTickerAsync(string symbol, string quoteSymbol)
    {
        var request = new BinanceFuturesRequestTicker(symbol, quoteSymbol);
        var ticker = await SendAsync<BinanceResponseTicker>(request);
        return Map<Ticker>(ticker);
    }

    public new Task<IEnumerable<Ticker>> GetTickersAsync()
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetTrades(symbol, quoteSymbol, default, default, limit);
    }

    public new async Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        var tradesRaw = await base.GetTrades(symbol, quoteSymbol, start, end, limit);
        var trades = JsonConvert.DeserializeObject<IEnumerable<BinanceResponseTrade>>(tradesRaw);
        return GroupTrades(trades);
    }

    public new async Task<Order?> PostOrder(Order order, bool test = false)
    {
        var newOrderJson = await base.PostOrder(order, test);
        var newOrder = JsonConvert.DeserializeObject<BinanceResponseOrder>(newOrderJson);
        return Map<Order>(newOrder);
    }

    public new async Task<Leverage> ChangeLeverage(string symbol, int leverage)
    {
        var leverageRaw = await base.ChangeLeverage(symbol, leverage);
        var newLeverage = JsonConvert.DeserializeObject<BinanceResponseLeverage>(leverageRaw);
        return Map<Leverage>(newLeverage);
    }

    public new async Task<Response> ChangeMarginTypeAsync(string symbol, string marginType)
    {
        var marginTypeRaw = await base.ChangeMarginTypeAsync(symbol, marginType);
        var response = JsonConvert.DeserializeObject<BinanceResponseObject>(marginTypeRaw);
        return Map<Response>(response);
    }

    public new async Task<Response> ChangePositionMarginAsync(string symbol, decimal amount, ChangePositionMargin type)
    {
        var positionMarginRaw = await base.ChangePositionMarginAsync(symbol, amount, type);
        var response = JsonConvert.DeserializeObject<BinanceResponseObject>(positionMarginRaw);
        return Map<Response>(response);
    }
}
