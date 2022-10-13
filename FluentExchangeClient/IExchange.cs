using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentExchangeClient;

public interface IExchange : IExchangeRaw
{
    new Task<Market?> GetMarketAsync(string symbol, string quoteSymbol);

    new Task<IEnumerable<Market>?> GetMarketsAsync();

    new Task<Ticker?> GetTickerAsync(string symbol, string quoteSymbol);

    new Task<IEnumerable<Ticker>?> GetTickersAsync();

    new Task<IEnumerable<Balance>?> GetBalancesAsync();

    new Task<Balance?> GetBalanceAsync(string symbol);

    new Task<DateTimeOffset?> GetServerTime();

    new Task<IEnumerable<Candle>?> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0);

    new Task<IDictionary<string, IEnumerable<Candle>>?> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0);

    new Task<Order?> GetOrder(string symbol, string quoteSymbol, string? orderId = null, string? clientOrderId = null);

    new Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, int limit = 0);

    new Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0);

    new Task<IEnumerable<Order>?> GetOpenOrders();

    new Task<IEnumerable<Order>?> GetOpenOrders(string symbol, string quoteSymbol);

    new Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, int limit = 0);

    new Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0);

    new Task<Order?> PostOrder(Order order);

    new Task<Order?> DeleteOrder(Order order);
}
