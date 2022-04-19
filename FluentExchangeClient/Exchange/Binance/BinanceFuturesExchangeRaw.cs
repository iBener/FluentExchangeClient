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

public class BinanceFuturesExchangeRaw : BinanceExchangeBase, IFuturesExchangeRaw
{
    public BinanceFuturesExchangeRaw(ExchangeOptions options) : base(options)
    {
    }

    public Task<string> ChangeLeverage(string symbol, int leverage)
    {
        var request = new BinanceFuturesRequestChangeLeverage(symbol, leverage, Timestamp, Options.Credentials);
        return SendAsync(request);
    }

    public Task<string> ChangeMarginTypeAsync(string symbol, string marginType)
    {
        var request = new BinanceFuturesRequestChangeMargin(symbol, marginType, Timestamp, Options.Credentials);
        return SendAsync(request);
    }

    public Task<string> ChangePositionMarginAsync(string symbol, decimal amount, ChangePositionMargin type)
    {
        var request = new BinanceFuturesRequestChangePositionMargin(symbol, amount, (int)type, Timestamp, Options.Credentials);
        return SendAsync(request);
    }

    public Task<string> DeleteOrder(Order order)
    {
        var request = new BinanceFuturesRequestDeleteOrder(new
        {
            symbol = order.Symbol,
            orderId = order.OrderId,
            origClientOrderId = order.ClientOrderId,
            timestamp = Timestamp
        }, Options.Credentials);
        return SendAsync(request);
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
        var request = new BinanceFuturesRequestBalance(Timestamp, Options.Credentials);
        return SendAsync(request);
    }

    public Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
    {
        var request = new BinanceFuturesRequestCandle(symbol, quoteSymbol, interval, limit);
        return SendAsync(request);
    }

    public Task<string> GetMarketAsync(string symbol, string quoteSymbol)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetMarketsAsync()
    {
        var request = new BinanceFuturesRequestExchangeInfo();
        return SendAsync(request);
    }

    public Task<string> GetOpenOrders()
    {
        return GetOpenOrders(String.Empty, String.Empty);
    }

    public Task<string> GetOpenOrders(string symbol, string quoteSymbol)
    {
        var request = new BinanceFuturesRequestOpenOrders(symbol, quoteSymbol, Timestamp, Options.Credentials);
        return SendAsync(request);
    }

    public Task<string> GetOrder(string symbol, string? orderId = null, string? clientOrderId = null)
    {
        var param = new
        {
            symbol,
            orderId,
            clientOrderId,
            timestamp = Timestamp
        };
        var request = new BinanceFuturesRequestGetOrder(param, Options.Credentials);
        return SendAsync(request);
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetOrders(symbol, quoteSymbol, default, default, limit);
    }

    public Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        limit = Math.Clamp(limit, 1, 1000);
        var request = new BinanceFuturesRequestOrders(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
        return SendAsync(request);
    }

    public override Task<string> GetServerTime()
    {
        var request = new BinanceRequestServerTime();
        return SendAsync(request);
    }

    public Task<string> GetTickerAsync(string symbol, string quoteSymbol)
    {
        var request = new BinanceFuturesRequestTicker(symbol, quoteSymbol);
        return SendAsync(request);
    }

    public Task<string> GetTickersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetTrades(symbol, quoteSymbol, default, default, limit);
    }

    public Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        var request = new BinanceFuturesRequestTrades(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
        return SendAsync(request);
    }

    public async Task<string> PostOrder(Order order, bool test = false)
    {
        var param = CreateParamObject(order);
        var request = new BinanceFuturesRequestPostOrder(param, Options.Credentials, test: test);
        if (!test)
        {
            string? result = await SendAsync(request);
            var resultOrder = JsonConvert.DeserializeObject<BinanceResponseOrder>(result);
            order.OrderId = resultOrder?.orderId.ToString() ?? String.Empty;
        }
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
        };
    }
}
