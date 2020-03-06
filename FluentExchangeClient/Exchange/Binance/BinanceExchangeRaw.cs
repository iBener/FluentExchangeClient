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

        public async Task<string> GetMarketsAsync()
        {
            var request = new BinanceRequestExchangeInfo();
            return await SendAsync(request);
        }

        public async Task<string> GetTickerAsync(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestTicker(symbol, quoteSymbol);
            return await SendAsync(request);
        }

        public async Task<string> GetTickersAsync()
        {
            var request = new BinanceRequestTicker();
            return await SendAsync(request);
        }

        public async Task<string> GetBalanceAsync(string symbol)
        {
            var json = await GetBalancesAsync();
            var balance = JObject.Parse(json).SelectToken($"$.balances[?(@.asset == '{symbol}')]");
            return JsonConvert.SerializeObject(balance);
        }

        public async Task<string> GetBalancesAsync()
        {
            var request = new BinanceRequestBalance(Timestamp, Options.Credentials);
            return await SendAsync(request);
        }

        public override async Task<string> GetServerTime()
        {
            var request = new BinanceRequestServerTime();
            return await SendAsync(request);
        }

        public async Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
        {
            var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
            return await SendAsync(request);
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

        public async Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
        {
            limit = Math.Clamp(limit, 1, 1000);
            var request = new BinanceRequestOrders(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
            return await SendAsync(request);
        }

        public Task<string> GetOpenOrders()
        {
            return GetOpenOrders(null, null);
        }

        public async Task<string> GetOpenOrders(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestOpenOrders(symbol, quoteSymbol, Timestamp, Options.Credentials);
            return await SendAsync(request);
        }

        public Task<string> PostTestOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<string> PostOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 500)
        {
            return GetTrades(symbol, quoteSymbol, default, default, limit);
        }

        public async Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
        {
            var request = new BinanceRequestTrades(symbol, quoteSymbol, start, end, Timestamp, limit, Options.Credentials);
            return await SendAsync(request);
        }
    }
}