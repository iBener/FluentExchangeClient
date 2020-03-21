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
            return response.serverTime; //DateTimeOffset.FromUnixTimeMilliseconds(response.serverTime);
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

        public new Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, int limit)
        {
            return GetOrders(symbol, quoteSymbol, default, default, limit);
        }

        public new async Task<IEnumerable<Order>> GetOrders(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit)
        {
            var ordersRaw = await base.GetOrders(symbol, quoteSymbol, start, end, limit);
            var orders = JsonConvert.DeserializeObject<IEnumerable<BinanceOrderResponse>>(ordersRaw);
            return Map<IEnumerable<Order>>(orders);
        }

        public new Task<IEnumerable<Order>> GetOpenOrders()
        {
            return GetOpenOrders(null, null);
        }

        public new async Task<IEnumerable<Order>> GetOpenOrders(string symbol, string quoteSymbol)
        {
            var ordersRaw = await base.GetOpenOrders(symbol, quoteSymbol);
            var orders = JsonConvert.DeserializeObject<IEnumerable<BinanceOrderResponse>>(ordersRaw);
            return Map<IEnumerable<Order>>(orders);
        }

        public new Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, int limit = 500)
        {
            return GetTrades(symbol, quoteSymbol, default, default, limit);
        }

        public new async Task<IEnumerable<Trade>> GetTrades(string symbol, string quoteSymbol, DateTime start, DateTime end, int limit = 500)
        {
            var tradesRaw = await base.GetTrades(symbol, quoteSymbol, start, end, limit);
            var trades = JsonConvert.DeserializeObject<IEnumerable<BinanceTradeResponse>>(tradesRaw);
            var result = new List<Trade>();
            var orders = new Dictionary<int, Trade>();
            foreach (var tradeResponse in trades)
            {
                var trade = Map<Trade>(tradeResponse);
                if (!orders.ContainsKey(trade.OrderId))
                {
                    trade.Commissions.Add(tradeResponse.commissionAsset, tradeResponse.commission);
                    ((List<TradeTransaction>)trade.Transactions)
                        .Add(new TradeTransaction { Quantity = trade.Quantity, QuoteQuantity = trade.QuoteQuantity, Time = trade.Time });
                    result.Add(trade);
                    orders.Add(trade.OrderId, trade);
                }
                else
                {
                    var mainTrade = orders[trade.OrderId];
                    if (!mainTrade.Commissions.ContainsKey(tradeResponse.commissionAsset))
                    {
                        mainTrade.Commissions.Add(tradeResponse.commissionAsset, tradeResponse.commission);
                    }
                    else
                    {
                        mainTrade.Time = trade.Time;
                        mainTrade.Quantity += trade.Quantity;
                        mainTrade.QuoteQuantity += trade.QuoteQuantity;
                        mainTrade.Commissions[tradeResponse.commissionAsset] += tradeResponse.commission;
                        ((List<TradeTransaction>)mainTrade.Transactions)
                            .Add(new TradeTransaction { Quantity = trade.Quantity, QuoteQuantity = trade.QuoteQuantity, Time = trade.Time });
                    }
                }
            }
            return result;
        }

        public new async Task<Order> PostTestOrder(Order order)
        {
            var newOrderJson = await base.PostTestOrder(order);
            return JsonConvert.DeserializeObject<Order>(newOrderJson);
        }
    }
}
