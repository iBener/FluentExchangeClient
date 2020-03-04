using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentExchangeClient
{
    public interface IExchange
    {
        string Name { get; }

        Task<Market> GetMarketAsync(string symbol, string quoteSymbol);

        Task<IEnumerable<Market>> GetMarketsAsync();

        Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol);

        Task<IEnumerable<Ticker>> GetTickersAsync();

        Task<Balance> GetBalanceAsync(string symbol);

        Task<IEnumerable<Balance>> GetBalancesAsync();

        Task<DateTimeOffset> GetServerTime();

        Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0);

        Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0);
    }
}
