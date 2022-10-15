using FluentExchangeClient.Builder;
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

class BinanceFuturesExchangeRaw : BinanceExchangeBase, IFuturesExchangeRaw
{
    public BinanceFuturesExchangeRaw(ExchangeOptions options) : base(options)
    {
    }

    public Task<string> ChangeLeverage(string symbol, int leverage)
    {
        var request = new BinanceFuturesRequestChangeLeverage(symbol, leverage, Timestamp, Options);
        return SendAsync(request);
    }

    public Task<string> ChangeMarginTypeAsync(string symbol, string marginType)
    {
        var request = new BinanceFuturesRequestChangeMargin(symbol, marginType, Timestamp, Options);
        return SendAsync(request);
    }

    public Task<string> ChangePositionMarginAsync(string symbol, decimal amount, ChangePositionMargin type)
    {
        var request = new BinanceFuturesRequestChangePositionMargin(symbol, amount, (int)type, Timestamp, Options);
        return SendAsync(request);
    }

    public Task<string> DeleteOrder(Order order)
    {
        var request = new BinanceFuturesRequestDeleteOrder(order.Symbol, order.QuoteSymbol, order.OrderId, order.ClientOrderId, Timestamp, Options);
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
                var request = new BinanceFuturesRequestCandle(symbol, quoteSymbol, interval, limit, Options);
                string? candles = await SendAsync(request);
                result[symbol + quoteSymbol] = candles;
            }
        }
        return result;
    }

    public async Task<string> GetBalanceAsync(string symbol)
    {
        var json = await GetBalancesAsync();
        var balance = JObject.Parse(json).SelectToken($"$.assets[?(@.asset == '{symbol}')]");
        return JsonConvert.SerializeObject(balance);
    }

    public Task<string> GetBalancesAsync()
    {
        var request = new BinanceFuturesRequestBalance(Timestamp, Options);
        return SendAsync(request);
    }

    public Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
    {
        var request = new BinanceFuturesRequestCandle(symbol, quoteSymbol, interval, limit, Options);
        return SendAsync(request);
    }

    public async Task<string> GetMarketAsync(string symbol, string quoteSymbol)
    {
        var response = await GetMarketsAsync();
        var market = JObject.Parse(response).SelectToken($"$.symbols[?(@.symbol == '{symbol + quoteSymbol}')]");
        return JsonConvert.SerializeObject(market);
    }

    public Task<string> GetMarketsAsync()
    {
        var request = new BinanceFuturesRequestExchangeInfo(Options);
        return SendAsync(request);
    }

    public Task<string> GetOpenOrders()
    {
        return GetOpenOrders(String.Empty, String.Empty);
    }

    public Task<string> GetOpenOrders(string symbol, string quoteSymbol)
    {
        var request = new BinanceFuturesRequestOpenOrders(symbol, quoteSymbol, Timestamp, Options);
        return SendAsync(request);
    }

    public Task<string> GetOrder(string symbol, string quoteSymbol, string? orderId = null, string? clientOrderId = null)
    {
        var request = new BinanceFuturesRequestGetOrder(symbol, quoteSymbol, orderId, clientOrderId, Timestamp, Options);
        return SendAsync(request);
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetOrders(symbol, quoteSymbol, default, default, limit: limit);
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        var request = new BinanceFuturesRequestOrders(symbol, quoteSymbol, start, end, limit, Timestamp, Options);
        return SendAsync(request);
    }

    public override Task<string> GetServerTime()
    {
        var options = new ExchangeOptions { UseTestServer = Options.UseTestServer };
        var request = new BinanceRequestServerTime(options);
        return SendAsync(request);
    }

    public Task<string> GetTickerAsync(string symbol, string quoteSymbol)
    {
        var request = new BinanceFuturesRequestTicker(symbol, quoteSymbol, Options);
        return SendAsync(request);
    }

    public Task<string> GetTickersAsync()
    {
        var request = new BinanceFuturesRequestTicker(Options);
        return SendAsync(request);
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetTrades(symbol, quoteSymbol, default, default, limit: limit);
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        var request = new BinanceFuturesRequestTrades(symbol, quoteSymbol, start, end, limit, Timestamp, Options);
        return SendAsync(request);
    }

    public async Task<string> PostOrder(Order order)
    {
        var param = CreateParamObject(order);
        var request = new BinanceFuturesRequestPostOrder(param, Options);
        string? result = await SendAsync(request);
        var resultOrder = JsonConvert.DeserializeObject<BinanceResponseOrder>(result);
        order.OrderId = resultOrder?.orderId.ToString() ?? String.Empty;
        return await GetOrder(order.Symbol, order.QuoteSymbol, orderId: order.OrderId, clientOrderId: order.ClientOrderId);
    }

    private object CreateParamObject(Order order)
    {
        return new
        {
            symbol = $"{order.Symbol}{order.QuoteSymbol}",
            side = order.Side,
            type = order.Type,
            quantity = order.Quantity,
            quoteOrderQty = order.QuoteQuantity,
            price = order.Price,
            timeInForce = order.TimeInForce,
            newClientOrderId = order.ClientOrderId,
            timestamp = Timestamp,
        };
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
