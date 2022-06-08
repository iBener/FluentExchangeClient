﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Bitfinex;

public class BitfinexExchange : BitfinexExchangeRaw, IExchange
{
    public BitfinexExchange(ExchangeOptions options) : base(options)
    {
    }

    public new Task<Order?> DeleteOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public new Task<IDictionary<string, IEnumerable<Candle>>?> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<Balance?> GetBalanceAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Balance>?> GetBalancesAsync()
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Candle>?> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<Market?> GetMarketAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Market>?> GetMarketsAsync()
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Order>?> GetOpenOrders()
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Order>?> GetOpenOrders(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public new Task<Order?> GetOrder(string symbol, string? orderId = null, string? clientOrderId = null)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<DateTimeOffset?> GetServerTime()
    {
        throw new NotImplementedException();
    }

    public new Task<Ticker?> GetTickerAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Ticker>?> GetTickersAsync()
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public new Task<Order?> PostOrder(Order order, bool test = false)
    {
        throw new NotImplementedException();
    }
}
