using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Bittrex
{
    class BittrexExchange : ExchangeBase, IExchange
    {
        public BittrexExchange(ExchangeOptions options) : base(options)
        {
        }

        public Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Balance> GetBalanceAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Balance>> GetBalancesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Market> GetMarketAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Market>> GetMarketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, string>> GetRawAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawBalanceAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawBalancesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawMarketAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawMarketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawServerTime()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawTickerAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRawTickersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetServerTime()
        {
            throw new NotImplementedException();
        }

        public Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ticker>> GetTickersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
