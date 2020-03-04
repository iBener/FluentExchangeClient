using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance
{
    class BinanceExchangeRaw : BinanceExchangeBase, IExchangeRaw
    {
        protected IExchangeRaw RawExchange => this;

        internal BinanceExchangeRaw(ExchangeOptions options) : base(options)
        {
        }

        async Task<IDictionary<string, string>> IExchangeRaw.GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
        {
            var marketsJson = await RawExchange.GetMarketsAsync();
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

        async Task<string> IExchangeRaw.GetBalanceAsync(string symbol)
        {
            var json = await RawExchange.GetBalancesAsync();
            var balance = JObject.Parse(json).SelectToken($"$.balances[?(@.asset == '{symbol}')]");
            return JsonConvert.SerializeObject(balance);
        }

        async Task<string> IExchangeRaw.GetBalancesAsync()
        {
            var parameters = new { timestamp = Timestamp };
            var request = new BinanceRequestBalance(parameters, Options.Credentials);
            return await SendAsync(request);
        }

        async Task<string> IExchangeRaw.GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
        {
            var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
            return await SendAsync(request);
        }

        async Task<string> IExchangeRaw.GetMarketAsync(string symbol, string quoteSymbol)
        {
            var response = await RawExchange.GetMarketsAsync();
            var market = JObject.Parse(response).SelectToken($"$.symbols[?(@.symbol == '{symbol + quoteSymbol}')]");
            return JsonConvert.SerializeObject(market);
        }

        async Task<string> IExchangeRaw.GetMarketsAsync()
        {
            var request = new BinanceRequestExchangeInfo();
            return await SendAsync(request);
        }

        Task<string> IExchangeRaw.GetServerTime()
        {
            return GetServerTime();
        }

        async Task<string> IExchangeRaw.GetTickerAsync(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestTicker(symbol, quoteSymbol);
            return await SendAsync(request);
        }

        async Task<string> IExchangeRaw.GetTickersAsync()
        {
            var request = new BinanceRequestTicker();
            return await SendAsync(request);
        }

        public override async Task<string> GetServerTime()
        {
            var request = new BinanceRequestServerTime();
            return await SendAsync(request);
        }
    }
}
