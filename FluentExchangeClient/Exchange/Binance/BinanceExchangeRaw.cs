using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

class BinanceExchangeRaw : BinanceExchangeBase, IExchangeRaw
{
    public BinanceExchangeRaw(ExchangeOptions options) : base(options)
    {
    }

    public async Task<string> GetMarketAsync(string symbol, string quoteSymbol)
    {
        var response = await GetMarketsAsync();
        var market = JObject.Parse(response).SelectToken($"$.symbols[?(@.symbol == '{symbol}{quoteSymbol}')]");
        return JsonConvert.SerializeObject(market);
    }

    public Task<string> GetMarketsAsync()
    {
        var request = new BinanceRequestExchangeInfo(Options);
        return SendAsync(request);
    }

    public Task<string> GetTickerAsync(string symbol, string quoteSymbol)
    {
        var request = new BinanceRequestTicker(symbol, quoteSymbol, Options);
        return SendAsync(request);
    }

    public Task<string> GetTickersAsync()
    {
        var request = new BinanceRequestTicker(Options);
        return SendAsync(request);
    }

    public Task<string> GetBalancesAsync()
    {
        var request = new BinanceRequestBalance(Options);
        return SendAsync(request);
    }

    public async Task<string> GetBalanceAsync(string symbol)
    {
        var json = await GetBalancesAsync();
        var balance = JObject.Parse(json).SelectToken($"$.balances[?(@.asset == '{symbol}')]");
        return JsonConvert.SerializeObject(balance);
    }

    public override Task<string> GetServerTime()
    {
        var request = new BinanceRequestServerTime(Options);
        return SendAsync(request);
    }

    public Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
    {
        var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit, Options);
        return SendAsync(request);
    }

    public async Task<IDictionary<string, string>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
    {
        var marketsJson = await GetMarketsAsync();
        var markets = JObject.Parse(marketsJson).SelectTokens($"$.symbols[?(@.quoteAsset == '{quoteSymbol}')]");
        var result = new Dictionary<string, string>();
        foreach (var market in markets)
        {
            string? symbol = market?["baseAsset"]?.Value<string>();
            if (symbol != null)
            {
                var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit, Options);
                string? candles = await SendAsync(request);
                result[$"{symbol}{quoteSymbol}"] = candles;
            }
        }
        return result;
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, int limit = 500)
    {
        return GetOrders(symbol, quoteSymbol, default, default, limit: limit);
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
    {
        limit = Math.Clamp(limit, 1, 1000);
        var request = new BinanceRequestOrders(symbol, quoteSymbol, start, end, Timestamp, limit, Options);
        return SendAsync(request);
    }

    public Task<string> GetOpenOrders()
    {
        return GetOpenOrders(String.Empty, String.Empty);
    }

    public Task<string> GetOpenOrders(string symbol, string quoteSymbol)
    {
        var request = new BinanceRequestOpenOrders(symbol, quoteSymbol, Options);
        return SendAsync(request);
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 500)
    {
        return GetTrades(symbol, quoteSymbol, default, default, limit: limit);
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
    {
        var request = new BinanceRequestTrades(symbol, quoteSymbol, start, end, Timestamp, limit, Options);
        return SendAsync(request);
    }

    public Task<string> GetOrder(string symbol, string quoteSymbol, string? orderId = null, string? clientOrderId = null)
    {
        var param = new
        {
            symbol = $"{symbol}{quoteSymbol}",
            orderId,
            clientOrderId,
            timestamp = Timestamp
        };
        return SendAsync(new BinanceRequestGetOrder(param, Options));
    }

    public async Task<string> PostOrder(Order order)
    {
        var param = CreateParamObject(order);
        var request = new BinanceRequestPostOrder(param, Options);
        await SendAsync(request);
        return await GetOrder(order.Symbol, order.OrderId, order.ClientOrderId);
    }

    private object CreateParamObject(Order order)
    {
        return new
        {
            symbol = order.Symbol,
            side = order.Side,
            type = order.Type,
            quantity = order.Quantity,
            quoteOrderQty = order.QuoteQuantity,
            price = order.Price,
            timeInForce = order.TimeInForce,
            newClientOrderId = order.ClientOrderId,
            timestamp = Timestamp,
            closePosition = order.ClosePosition
        };
    }

    public Task<string> DeleteOrder(Order order)
    {
        var request = new BinanceRequestDeleteOrder(order.Symbol, order.OrderId, order.ClientOrderId, Options);
        return SendAsync(request);
    }

    public Task<string> ClosePosition(string symbol)
    {

        throw new NotImplementedException();
    }

    public Task<string> OrderBook(string symbol, string quoteSymbol, int limit = 0)
    {
        throw new NotImplementedException();
    }
}
