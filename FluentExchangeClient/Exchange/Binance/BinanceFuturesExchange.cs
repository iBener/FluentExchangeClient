using FluentExchangeClient.Builder;
using FluentExchangeClient.Common;
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

public class BinanceFuturesExchange : BinanceFuturesExchangeRaw, IFuturesExchange
{
    public BinanceFuturesExchange(ExchangeOptions options) : base(options)
    {
    }

    public new async Task<Leverage?> ChangeLeverage(string symbol, int leverage)
    {
        var leverageRaw = await base.ChangeLeverage(symbol, leverage);
        var newLeverage = JsonConvert.DeserializeObject<BinanceResponseLeverage>(leverageRaw);
        return Map<Leverage>(newLeverage);
    }

    public new async Task<Response?> ChangeMarginTypeAsync(string symbol, string marginType)
    {
        var marginTypeRaw = await base.ChangeMarginTypeAsync(symbol, marginType);
        var response = JsonConvert.DeserializeObject<BinanceResponseObject>(marginTypeRaw);
        return Map<Response>(response);
    }

    public new async Task<Response?> ChangePositionMarginAsync(string symbol, decimal amount, ChangePositionMargin type)
    {
        var positionMarginRaw = await base.ChangePositionMarginAsync(symbol, amount, type);
        var response = JsonConvert.DeserializeObject<BinanceResponseObject>(positionMarginRaw);
        return Map<Response>(response);
    }

    public new async Task<IDictionary<string, IEnumerable<Candle>>?> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
    {
        var marketsJson = await base.GetMarketsAsync();
        var markets = JObject.Parse(marketsJson).SelectTokens($"$.symbols[?(@.quoteAsset == '{quoteSymbol}')]");
        var result = new Dictionary<string, IEnumerable<Candle>>();
        if (markets != null)
        {
            foreach (var market in markets)
            {
                string? symbol = market?["baseAsset"]?.Value<string>();
                if (!String.IsNullOrEmpty(symbol))
                {
                    var candles = await GetCandlesAsync(symbol, quoteSymbol, interval, limit);
                    if (candles != null)
                    {
                        result[symbol + quoteSymbol] = candles;
                    }
                }
            }
        }
        return result;
    }

    public new async Task<Balance?> GetBalanceAsync(string symbol)
    {
        var balanceResponse = await base.GetBalanceAsync(symbol);
        var balance = JsonConvert.DeserializeObject<BinanceFuturesResponseAccountAssets>(balanceResponse);
        return Map<Balance>(balance);
    }

    public new async Task<IEnumerable<Balance>?> GetBalancesAsync()
    {
        var response = await base.GetBalancesAsync();
        var account = JsonConvert.DeserializeObject<BinanceFuturesResponseAccount>(response);
        return Map<IEnumerable<Balance>>(account?.assets.Where(x => x.marginBalance > 0));
    }

    public new async Task<IEnumerable<Candle>?> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
    {
        var response = await base.GetCandlesAsync(symbol, quoteSymbol, interval, limit);
        var candles = JsonConvert.DeserializeObject<IEnumerable<BinanceCandleResponse>>(response);
        var result = Map<IEnumerable<Candle>>(candles);
        if (result != null)
        {
            foreach (var candle in result)
            {
                candle.Base = symbol;
                candle.Quote = quoteSymbol;
            }
        }
        return result;
    }

    public new async Task<Market?> GetMarketAsync(string symbol, string quoteSymbol)
    {
        var markets = await GetMarketsAsync();
        return markets?.FirstOrDefault(x => x.Base == symbol && x.Quote == quoteSymbol);
    }

    public new async Task<IEnumerable<Market>?> GetMarketsAsync()
    {
        var response = await base.GetMarketsAsync();
        var markets = JsonConvert.DeserializeObject<BinanceResponseExchangeInfo>(response);
        return Map<IEnumerable<Market>>(markets?.symbols.Where(x => x.symbol.Equals(x.pair)));
    }

    public new Task<IEnumerable<Order>?> GetOpenOrders()
    {
        return GetOpenOrders(String.Empty, String.Empty);
    }

    public new async Task<IEnumerable<Order>?> GetOpenOrders(string symbol, string quoteSymbol)
    {
        var response = await base.GetOpenOrders(symbol, quoteSymbol);
        var orders = JsonConvert.DeserializeObject<IEnumerable<BinanceResponseOrder>>(response);
        return Map<IEnumerable<Order>>(orders);
    }

    public new async Task<Order?> GetOrder(string symbol, string quoteSymbol, string? orderId = null, string? clientOrderId = null)
    {
        var orderRaw = await base.GetOrder(symbol, quoteSymbol, orderId, clientOrderId);
        var order = JsonConvert.DeserializeObject<BinanceResponseOrder>(orderRaw);
        return Map<Order>(order);
    }

    public new Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetOrders(symbol, quoteSymbol, default, default, limit);
    }

    public new async Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        var respose = await base.GetOrders(symbol, quoteSymbol, start, end, limit);
        var orders = JsonConvert.DeserializeObject<IEnumerable<BinanceResponseOrder>>(respose);
        return Map<IEnumerable<Order>>(orders);
    }

    public new async Task<DateTimeOffset?> GetServerTime()
    {
        var response = await base.GetServerTime();
        var time = JsonConvert.DeserializeObject<BinanceResponseServerTime>(response);
        return time?.serverTime;
    }

    public new async Task<Ticker?> GetTickerAsync(string symbol, string quoteSymbol)
    {
        var response = await base.GetTickerAsync(symbol, quoteSymbol);
        var ticker = JsonConvert.DeserializeObject<BinanceResponseTicker>(response);
        return Map<Ticker>(ticker);
    }

    public new async Task<IEnumerable<Ticker>?> GetTickersAsync()
    {
        var response = await base.GetTickersAsync();
        var tickers = JsonConvert.DeserializeObject<IEnumerable<Ticker>>(response);
        return Map<IEnumerable<Ticker>>(tickers);
    }

    public new Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, int limit = 0)
    {
        return GetTrades(symbol, quoteSymbol, default, default, limit);
    }

    public new async Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
    {
        var response = await base.GetTrades(symbol, quoteSymbol, start, end, limit: limit);
        var trades = JsonConvert.DeserializeObject<IEnumerable<BinanceResponseTrade>>(response);
        return GroupTrades(trades);
    }

    public new async Task<Order?> PostOrder(Order order)
    {
        var newOrderJson = await base.PostOrder(order);
        var newOrder = JsonConvert.DeserializeObject<BinanceResponseOrder>(newOrderJson);
        return Map<Order>(newOrder);
    }

    public new async Task<Order?> DeleteOrder(Order order)
    {
        var canceledOrderJson = await base.DeleteOrder(order);
        var canceledOrder = JsonConvert.DeserializeObject<BinanceResponseOrderDelete>(canceledOrderJson);
        return Map<Order>(canceledOrder);
    }
}
