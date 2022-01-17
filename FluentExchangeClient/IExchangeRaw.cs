using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentExchangeClient;

public interface IExchangeRaw
{
    string Name { get; }

    Task<string> GetMarketAsync(string symbol, string quoteSymbol);

    Task<string> GetMarketsAsync();

    Task<string> GetTickerAsync(string symbol, string quoteSymbol);

    Task<string> GetTickersAsync();

    Task<string> GetBalanceAsync(string symbol);

    Task<string> GetBalancesAsync();

    Task<string> GetServerTime();

    Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0);

    Task<IDictionary<string, string>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0);

    Task<string> GetOrder(string symbol, string orderId = null, string clientOrderId = null);

    Task<string> GetOrders(string symbol, string quoteSymbol, int limit = 0);

    Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0);

    Task<string> GetOpenOrders();

    Task<string> GetOpenOrders(string symbol, string quoteSymbol);

    Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 0);

    Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0);

    Task<string> PostOrder(Order order, bool test = false);

    Task<string> DeleteOrder(Order order);
}
