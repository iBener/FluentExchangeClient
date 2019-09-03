using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Internal;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance
{
    class BinanceExchange : ExchangeBase, IExchange
    {

        internal BinanceExchange(ExchangeOptions options) : base(options)
        {
        }

        private DateTimeOffset serverTime;

        private static double? serverTimeDiff = null;

        private long Timestamp
        {
            get
            {
                if (serverTimeDiff == null)
                {
                    serverTime = GetServerTime().Result;
                    serverTimeDiff = (serverTime - DateTimeOffset.UtcNow).TotalMilliseconds;
                }
                var now = DateTimeOffset.UtcNow.AddMilliseconds(serverTimeDiff.Value);
                return now.ToUnixTimeMilliseconds();
            }
        }

        #region IExchangeApi

        public async Task<Balance> GetBalanceAsync(string symbol)
        {
            var balances = await GetBalancesAsync();
            return balances.FirstOrDefault(x => x.Symbol == symbol);
        }

        public async Task<string> GetRawBalanceAsync(string symbol)
        {
            var json = await GetRawBalancesAsync();
            var balance = JObject.Parse(json).SelectToken($"$.balances[?(@.asset == '{symbol}')]");
            return JsonConvert.SerializeObject(balance);
        }

        public async Task<IEnumerable<Balance>> GetBalancesAsync()
        {
            var response = await GetRawBalancesAsync();
            var account = JsonConvert.DeserializeObject<BinanceAccountResponse>(response);
            return Map<IEnumerable<Balance>>(account.balances.Where(x => x.free + x.locked > 0));
        }

        public async Task<string> GetRawBalancesAsync()
        {
            var parameters = new { timestamp = Timestamp };
            var request = new BinanceRequestBalance(parameters, Options.Credentials);
            return await SendAsync(request);
        }

        public async Task<Market> GetMarketAsync(string symbol, string quoteSymbol)
        {
            var markets = await GetMarketsAsync();
            return markets.FirstOrDefault(x => x.Base == symbol && x.Quote == quoteSymbol);
        }

        public async Task<string> GetRawMarketAsync(string symbol, string quoteSymbol)
        {
            var response = await GetRawMarketsAsync();
            var market = JObject.Parse(response).SelectToken($"$.symbols[?(@.symbol == '{symbol + quoteSymbol}')]");
            return JsonConvert.SerializeObject(market);
        }

        public async Task<IEnumerable<Market>> GetMarketsAsync()
        {
            var response = await GetRawMarketsAsync();
            var markets = JsonConvert.DeserializeObject<BinanceExchangeInfoResponse>(response);
            return Map<IEnumerable<Market>>(markets.symbols);
        }

        public async Task<string> GetRawMarketsAsync()
        {
            var request = new BinanceRequestExchangeInfo();
            return await SendAsync(request);
        }

        public async Task<DateTimeOffset> GetServerTime()
        {
            var request = new BinanceRequestServerTime();
            var response = await SendAsync<BinanceServerTimeResponse>(request);
            return DateTimeOffset.FromUnixTimeMilliseconds(response.serverTime);
        }

        public async Task<string> GetRawServerTime()
        {
            var request = new BinanceRequestServerTime();
            return await SendAsync(request);
        }

        public async Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestTicker(symbol, quoteSymbol);
            var response = await SendAsync<BinanceTickerResponse>(request);
            return Map<Ticker>(response);
        }

        public async Task<string> GetRawTickerAsync(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestTicker(symbol, quoteSymbol);
            return await SendAsync(request);
        }

        public async Task<IEnumerable<Ticker>> GetTickersAsync()
        {
            var request = new BinanceRequestTicker();
            var tickers = await SendAsync<List<BinanceTickerResponse>>(request);
            return Map<IEnumerable<Ticker>>(tickers);
        }

        public async Task<string> GetRawTickersAsync()
        {
            var request = new BinanceRequestTicker();
            return await SendAsync(request);
        }

        public async Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
        {
            var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
            var candles = await SendAsync<IEnumerable<BinanceCandleResponse>>(request);
            return Map<IEnumerable<Candle>>(candles);
        }

        public async Task<string> GetRawCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
        {
            var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
            return await SendAsync(request);
        }

        public async Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 500)
        {
            var marketsJson = await GetRawMarketsAsync();
            var markets = JObject.Parse(marketsJson).SelectTokens($"$.symbols[?(@.quoteAsset == '{quoteSymbol}')]");
            var result = new Dictionary<string, IEnumerable<Candle>>();
            foreach (var market in markets)
            {
                var symbol = market["baseAsset"].Value<string>();
                var candles = await GetCandlesAsync(symbol, quoteSymbol, interval, limit);
                result[symbol + quoteSymbol] = candles;
            }
            return result;
        }

        public async Task<IDictionary<string, string>> GetRawAllCandlesAsync(string quoteSymbol, string interval, int limit = 500)
        {
            var marketsJson = await GetRawMarketsAsync();
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

        #endregion
    }
}
