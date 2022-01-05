using FluentExchangeClient.Builder;
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

    public Task DeleteOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
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
        var account = JsonConvert.DeserializeObject<BinanceResponseAccount>(response);
        return Map<IEnumerable<Balance>>(account.balances.Where(x => x.free + x.locked > 0));
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

    public Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
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
        var request = new BinanceRequestServerTime();
        var response = await SendAsync<BinanceResponseServerTime>(request);
        return response.serverTime;
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
}
