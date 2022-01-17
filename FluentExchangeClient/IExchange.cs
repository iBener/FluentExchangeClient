﻿using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentExchangeClient;

public interface IExchange
{
    string Name { get; }

    Task<Market> GetMarketAsync(string symbol, string quoteSymbol);

    Task<IEnumerable<Market>> GetMarketsAsync();

    Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol);

    Task<IEnumerable<Ticker>> GetTickersAsync();

    Task<IEnumerable<Balance>> GetBalancesAsync();

    Task<Balance> GetBalanceAsync(string symbol);

    Task<DateTimeOffset> GetServerTime();

    Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0);

    Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0);

    Task<Order> GetOrder(string symbol, string orderId = null, string clientOrderId = null);

    Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, int limit = 0);

    Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0);

    Task<IEnumerable<Order>> GetOpenOrders();

    Task<IEnumerable<Order>> GetOpenOrders(string symbol, string quoteSymbol);

    Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, int limit = 0);

    Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0);

    Task<Order> PostOrder(Order order, bool test = false);

    Task DeleteOrder(Order order);
}
