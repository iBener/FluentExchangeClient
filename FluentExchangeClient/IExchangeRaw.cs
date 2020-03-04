using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentExchangeClient
{
    public interface IExchangeRaw
    {
        string Name { get; }

        Task<string> GetMarketAsync(string symbol, string quoteSymbol);

        Task<string> GetMarketsAsync();

        Task<string> GetTickerAsync(string symbol, string quoteSymbol);

        Task<string> GetTickersAsync();

        Task<string> GetBalanceAsync(string symbol);

        Task<string> GetBalancesAsync();

        Task<string> GetServerTime();

        Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0);

        Task<IDictionary<string, string>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0);
    }
}
