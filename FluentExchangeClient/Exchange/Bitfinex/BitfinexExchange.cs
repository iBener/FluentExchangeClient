using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Bitfinex;

public class BitfinexExchange : ExchangeBase, IExchange
{
    public BitfinexExchange(ExchangeOptions options) : base(options)
    {
    }

    public Task<Order> DeleteOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<Balance> GetBalanceAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Balance>> GetBalancesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<Market> GetMarketAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Market>> GetMarketsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOpenOrders()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOpenOrders(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrder(string symbol, string orderId = null, string clientOrderId = null)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public override Task<string> GetServerTime()
    {
        throw new NotImplementedException();
    }

    public Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Ticker>> GetTickersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<Order> PostOrder(Order order, bool test = false)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.DeleteOrder(Order order)
    {
        throw new NotImplementedException();
    }

    Task<IDictionary<string, string>> IExchangeRaw.GetAllCandlesAsync(string quoteSymbol, string interval, int limit)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetBalanceAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetBalancesAsync()
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetMarketAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetMarketsAsync()
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetOpenOrders()
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetOpenOrders(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetOrder(string symbol, string orderId, string clientOrderId)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetOrders(string symbol, string quoteSymbol, int limit)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit)
    {
        throw new NotImplementedException();
    }

    Task<DateTimeOffset> IExchange.GetServerTime()
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetTickerAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetTickersAsync()
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetTrades(string symbol, string quoteSymbol, int limit)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit)
    {
        throw new NotImplementedException();
    }

    Task<string> IExchangeRaw.PostOrder(Order order, bool test)
    {
        throw new NotImplementedException();
    }
}
