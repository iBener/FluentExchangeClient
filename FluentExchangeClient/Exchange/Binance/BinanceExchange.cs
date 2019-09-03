using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Exchange.Binance.Requests;
using FluentExchangeClient.Exchange.Binance.Responses;
using FluentExchangeClient.Models;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FluentExchangeClient.Internal.Binance
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
            var balances = account.balances;

            throw new NotImplementedException();
        }

        public async Task<string> GetRawBalancesAsync()
        {
            var parameters = new { timestamp = Timestamp };
            var request = new BinanceRequestBalance(parameters, Options.Credentials);
            return await SendAsync(request);
        }

        public async Task<Market> GetMarketAsync(string symbol, string baseSymbol)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRawMarketAsync(string symbol, string baseSymbol)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Market>> GetMarketsAsync()
        {
            var response = await GetRawMarketsAsync();
            var markets = JsonConvert.DeserializeObject<BinanceExchangeInfoResponse>(response);

            throw new NotImplementedException();
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

        public async Task<Ticker> GetTickerAsync(string pair)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRawTickerAsync(string pair)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ticker>> GetTickersAsync()
        {
            var request = new BinanceRequestTicker();
            var tickers = await SendAsync<List<BinanceTickerResponse>>(request);

            throw new NotImplementedException();
        }

        public async Task<string> GetRawTickersAsync()
        {
            var request = new BinanceRequestTicker();
            return await SendAsync(request);
        }

        public async Task<Candle> GetCandleAsync(string pair)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRawCandleAsync(string pair)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Candle>> GetCandlesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRawCandlesAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
