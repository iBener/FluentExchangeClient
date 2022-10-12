using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Bitfinex
{
    public class BitfinexExchangeRaw : BitfinexExchangeBase, IExchangeRaw
    {
        public BitfinexExchangeRaw(ExchangeOptions options) : base(options)
        {
        }

        public Task<string> DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, string>> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetBalanceAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetBalancesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMarketAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMarketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOpenOrders(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOrder(string symbol, string quaoteSymbol, string? orderId = null, string? clientOrderId = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOrders(string symbol, string quoteSymbol, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTickerAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTickersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTrades(string symbol, string quoteSymbol, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> PostOrder(Order order, bool test = false)
        {
            throw new NotImplementedException();
        }

        public Task<string> ClosePosition(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> OrderBook(string symbol, string quoteSymbol, int limit = 0)
        {
            throw new NotImplementedException();
        }
    }
}
