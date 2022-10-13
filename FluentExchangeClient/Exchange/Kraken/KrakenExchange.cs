using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Kraken
{
    public class KrakenExchange : KrakenExchangeRaw, IExchange
    {
        public KrakenExchange(ExchangeOptions options) : base(options)
        {
        }

        public new Task<Order?> DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public new Task<IDictionary<string, IEnumerable<Candle>>?> GetAllCandlesAsync(string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public new Task<Balance?> GetBalanceAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Balance>?> GetBalancesAsync()
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Candle>?> GetCandlesAsync(string symbol, string quoteSymbol, string interval, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public new Task<Market?> GetMarketAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Market>?> GetMarketsAsync()
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Order>?> GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Order>?> GetOpenOrders(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public new Task<Order?> GetOrder(string symbol, string quaoteSymbol, string? orderId = null, string? clientOrderId = null)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Order>?> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public new Task<DateTimeOffset?> GetServerTime()
        {
            throw new NotImplementedException();
        }

        public new Task<Ticker?> GetTickerAsync(string symbol, string quoteSymbol)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Ticker>?> GetTickersAsync()
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<Trade>?> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 0)
        {
            throw new NotImplementedException();
        }

        public new Task<Order?> PostOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
