using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance
{
    public class BinanceExchange : BinanceExchangeRaw, IExchange
    {
        public BinanceExchange(ExchangeOptions options) : base(options)
        {
        }

        public new async Task<Market> GetMarketAsync(string symbol, string quoteSymbol)
        {
            var markets = await GetMarketsAsync();
            return markets.FirstOrDefault(x => x.Base == symbol && x.Quote == quoteSymbol);
        }

        public new async Task<IEnumerable<Market>> GetMarketsAsync()
        {
            var response = await base.GetMarketsAsync();
            var markets = JsonConvert.DeserializeObject<BinanceExchangeInfoResponse>(response);
            return Map<IEnumerable<Market>>(markets.symbols);
        }

        public new async Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestTicker(symbol, quoteSymbol);
            var response = await SendAsync<BinanceTickerResponse>(request);
            return Map<Ticker>(response);
        }

        public new async Task<IEnumerable<Ticker>> GetTickersAsync()
        {
            var request = new BinanceRequestTicker();
            var tickers = await SendAsync<List<BinanceTickerResponse>>(request);
            return Map<IEnumerable<Ticker>>(tickers);
        }

        public new async Task<Balance> GetBalanceAsync(string symbol)
        {
            var balances = await GetBalancesAsync();
            return balances.FirstOrDefault(x => x.Symbol == symbol);
        }

        public new async Task<IEnumerable<Balance>> GetBalancesAsync()
        {
            var response = await base.GetBalancesAsync();
            var account = JsonConvert.DeserializeObject<BinanceAccountResponse>(response);
            return Map<IEnumerable<Balance>>(account.balances.Where(x => x.free + x.locked > 0));
        }

        public new async Task<DateTimeOffset> GetServerTime()
        {
            var request = new BinanceRequestServerTime();
            var response = await SendAsync<BinanceServerTimeResponse>(request);
            return DateTimeOffset.FromUnixTimeMilliseconds(response.serverTime);
        }

        public new async Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
        {
            var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
            var candles = await SendAsync<IEnumerable<BinanceCandleResponse>>(request);
            return Map<IEnumerable<Candle>>(candles);
        }

        public new async Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 500)
        {
            var marketsJson = await base.GetMarketsAsync();
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
    }
}
