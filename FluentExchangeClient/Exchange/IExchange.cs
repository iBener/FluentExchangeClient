using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Internal
{
    public interface IExchange
    {
        string Name { get; }

        Task<Market> GetMarketAsync(string symbol, string quoteSymbol);
        Task<string> GetRawMarketAsync(string symbol, string quoteSymbol);

        Task<IEnumerable<Market>> GetMarketsAsync();
        Task<string> GetRawMarketsAsync();

        Task<Ticker> GetTickerAsync(string symbol, string quoteSymbol);
        Task<string> GetRawTickerAsync(string symbol, string quoteSymbol);

        Task<IEnumerable<Ticker>> GetTickersAsync();
        Task<string> GetRawTickersAsync();

        Task<Balance> GetBalanceAsync(string symbol);
        Task<string> GetRawBalanceAsync(string symbol);

        Task<IEnumerable<Balance>> GetBalancesAsync();
        Task<string> GetRawBalancesAsync();

        Task<DateTimeOffset> GetServerTime();
        Task<string> GetRawServerTime();

        Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0);
        Task<string> GetRawCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0);

        Task<IDictionary<string, IEnumerable<Candle>>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0);
        Task<IDictionary<string, string>> GetRawAllCandlesAsync(string quoteSymbol, string interval, int limit = 0);
    }
}
