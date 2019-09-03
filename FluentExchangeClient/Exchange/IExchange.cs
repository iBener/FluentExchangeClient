using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Internal
{
    public interface IExchange
    {
        Task<Market> GetMarketAsync(string symbol, string baseSymbol);
        Task<string> GetRawMarketAsync(string symbol, string baseSymbol);

        Task<IEnumerable<Market>> GetMarketsAsync();
        Task<string> GetRawMarketsAsync();

        Task<Ticker> GetTickerAsync(string pair);
        Task<string> GetRawTickerAsync(string pair);

        Task<IEnumerable<Ticker>> GetTickersAsync();
        Task<string> GetRawTickersAsync();

        Task<Balance> GetBalanceAsync(string symbol);
        Task<string> GetRawBalanceAsync(string symbol);

        Task<IEnumerable<Balance>> GetBalancesAsync();
        Task<string> GetRawBalancesAsync();

        Task<DateTimeOffset> GetServerTime();
        Task<string> GetRawServerTime();

        Task<Candle> GetCandleAsync(string pair);
        Task<string> GetRawCandleAsync(string pair);

        Task<IEnumerable<Candle>> GetCandlesAsync();
        Task<string> GetRawCandlesAsync();
    }
}
