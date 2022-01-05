using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

public class BinancePerpetualExchangeRaw : BinanceExchangeBase, IExchangeRaw
{
    public BinancePerpetualExchangeRaw(ExchangeOptions options) : base(options)
    {
    }

    public Task<string> DeleteOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IDictionary<string, string>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetBalanceAsync(string symbol)
    {
        var json = await GetBalancesAsync();
        var balance = JObject.Parse(json).SelectToken($"$.assets[?(@.asset == '{symbol}')]");
        return JsonConvert.SerializeObject(balance);
    }

    public Task<string> GetBalancesAsync()
    {
        var request = new BinanceRequestPerpetualBalance(Timestamp, Options.Credentials);
        return SendAsync(request);
    }

    public Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
    {
        var request = new BinanceRequestPerpetualCandle(symbol, quoteSymbol, interval, limit);
        return SendAsync(request);
    }

    public Task<string> GetMarketAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetMarketsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetOpenOrders()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetOpenOrders(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public override Task<string> GetServerTime()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetTickerAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetTickersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<string> PostOrder(Order order, bool test = false)
    {
        throw new NotImplementedException();
    }
}
