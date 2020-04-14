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

namespace FluentExchangeClient.Exchange.Binance
{
    public class BinanceExchangeRaw : BinanceExchangeBase, IExchangeRaw
    {
        public BinanceExchangeRaw(ExchangeOptions options) : base(options)
        {
        }

        public async Task<string> GetMarketAsync(string symbol, string quoteSymbol)
        {
            var response = await GetMarketsAsync();
            var market = JObject.Parse(response).SelectToken($"$.symbols[?(@.symbol == '{symbol + quoteSymbol}')]");
            return JsonConvert.SerializeObject(market);
        }

        public Task<string> GetMarketsAsync()
        {
            var request = new BinanceRequestExchangeInfo();
            return SendAsync(request);
        }

        public Task<string> GetTickerAsync(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestTicker(symbol, quoteSymbol);
            return SendAsync(request);
        }

        public Task<string> GetTickersAsync()
        {
            var request = new BinanceRequestTicker();
            return SendAsync(request);
        }

        public async Task<string> GetBalanceAsync(string symbol)
        {
            var json = await GetBalancesAsync();
            var balance = JObject.Parse(json).SelectToken($"$.balances[?(@.asset == '{symbol}')]");
            return JsonConvert.SerializeObject(balance);
        }

        public Task<string> GetBalancesAsync()
        {
            var request = new BinanceRequestBalance(Timestamp, Options.Credentials);
            return SendAsync(request);
        }

        public override Task<string> GetServerTime()
        {
            var request = new BinanceRequestServerTime();
            return SendAsync(request);
        }

        public Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
        {
            var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
            return SendAsync(request);
        }

        public async Task<IDictionary<string, string>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
        {
            var marketsJson = await GetMarketsAsync();
            var markets = JObject.Parse(marketsJson).SelectTokens($"$.symbols[?(@.quoteAsset == '{quoteSymbol}')]");
            var result = new Dictionary<string, string>();
            foreach (var market in markets)
            {
                var symbol = market["baseAsset"].Value<string>();
                var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
                var candles = await SendAsync(request);
                result[symbol + quoteSymbol] = candles;
            }
            return result;
        }

        public Task<string> GetOrders(string symbol, string quoteSymbol, int limit = 500)
        {
            return GetOrders(symbol, quoteSymbol, default, default, limit);
        }

        public Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
        {
            limit = Math.Clamp(limit, 1, 1000);
            var request = new BinanceRequestOrders(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
            return SendAsync(request);
        }

        public Task<string> GetOpenOrders()
        {
            return GetOpenOrders(null, null);
        }

        public Task<string> GetOpenOrders(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestOpenOrders(symbol, quoteSymbol, Timestamp, Options.Credentials);
            return SendAsync(request);
        }

        public Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 500)
        {
            return GetTrades(symbol, quoteSymbol, default, default, limit);
        }

        public Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
        {
            var request = new BinanceRequestTrades(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
            return SendAsync(request);
        }

        public Task<string> GetOrder(Order order)
        {
            var param = new
            {
                symbol = order.Symbol,
                orderId = order.OrderId,
                origClientOrderId = order.ClientOrderId,
                timestamp = Timestamp
            };
            return SendAsync(new BinanceRequestGetOrder(param, Options.Credentials));
        }

        public Task<string> PostOrder(Order order, bool test = false)
        {
            var param = CreateObjectParamObject(order);
            var request = new BinanceRequestPostOrder(param, Options.Credentials, test: test);
            if (!test && !String.IsNullOrEmpty(order.ClientOrderId))
            {
                Task.WaitAll(SendAsync(request));
                return GetOrder(order);
            }
            return SendAsync(request);
        }

        private object CreateObjectParamObject(Order order)
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

        public Task<string> DeleteOrder(Order order)
        {
            var request = new BinanceRequestDeleteOrder(new 
            {
                symbol = order.Symbol,
                orderId = order.OrderId,
                origClientOrderId = order.ClientOrderId,
                timestamp = Timestamp
            }, Options.Credentials);
            return SendAsync(request);
        }
    }
}