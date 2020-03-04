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
    class BinanceExchange : BinanceExchangeRaw, IExchange
    {
        IExchange Exchange => this;

        internal BinanceExchange(ExchangeOptions options) : base(options)
        {
        }

        async Task<Balance> IExchange.GetBalanceAsync(string symbol)
        {
            var balances = await Exchange.GetBalancesAsync();
            return balances.FirstOrDefault(x => x.Symbol == symbol);
        }

        async Task<IEnumerable<Balance>> IExchange.GetBalancesAsync()
        {
            var response = await RawExchange.GetBalancesAsync();
            var account = JsonConvert.DeserializeObject<BinanceAccountResponse>(response);
            return Map<IEnumerable<Balance>>(account.balances.Where(x => x.free + x.locked > 0));
        }

        async Task<Market> IExchange.GetMarketAsync(string symbol, string quoteSymbol)
        {
            var markets = await Exchange.GetMarketsAsync();
            return markets.FirstOrDefault(x => x.Base == symbol && x.Quote == quoteSymbol);
        }

        async Task<IEnumerable<Market>> IExchange.GetMarketsAsync()
        {
            var response = await RawExchange.GetMarketsAsync();
            var markets = JsonConvert.DeserializeObject<BinanceExchangeInfoResponse>(response);
            return Map<IEnumerable<Market>>(markets.symbols);
        }

        async Task<DateTimeOffset> IExchange.GetServerTime()
        {
            var request = new BinanceRequestServerTime();
            var response = await SendAsync<BinanceServerTimeResponse>(request);
            return DateTimeOffset.FromUnixTimeMilliseconds(response.serverTime);
        }

        async Task<Ticker> IExchange.GetTickerAsync(string symbol, string quoteSymbol)
        {
            var request = new BinanceRequestTicker(symbol, quoteSymbol);
            var response = await SendAsync<BinanceTickerResponse>(request);
            return Map<Ticker>(response);
        }

        async Task<IEnumerable<Ticker>> IExchange.GetTickersAsync()
        {
            var request = new BinanceRequestTicker();
            var tickers = await SendAsync<List<BinanceTickerResponse>>(request);
            return Map<IEnumerable<Ticker>>(tickers);
        }

        async Task<IEnumerable<Candle>> IExchange.GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 500)
        {
            var request = new BinanceRequestCandle(symbol, quoteSymbol, interval, limit);
            var candles = await SendAsync<IEnumerable<BinanceCandleResponse>>(request);
            return Map<IEnumerable<Candle>>(candles);
        }

        async Task<IDictionary<string, IEnumerable<Candle>>> IExchange.GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 500)
        {
            var marketsJson = await RawExchange.GetMarketsAsync();
            var markets = JObject.Parse(marketsJson).SelectTokens($"$.symbols[?(@.quoteAsset == '{quoteSymbol}')]");
            var result = new Dictionary<string, IEnumerable<Candle>>();
            foreach (var market in markets)
            {
                var symbol = market["baseAsset"].Value<string>();
                var candles = await Exchange.GetCandlesAsync(symbol, quoteSymbol, interval, limit);
                result[symbol + quoteSymbol] = candles;
            }
            return result;
        }
    }
}
