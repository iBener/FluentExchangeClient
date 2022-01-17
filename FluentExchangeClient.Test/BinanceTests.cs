using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FluentExchangeClient.Test;

public class BinanceTests
{
    IExchange exchange;

    [SetUp]
    public void Setup()
    {
        var settings = File.ReadAllText("binance_settings.json");
        var obj = JObject.Parse(settings);
        if (File.Exists("binance_settings.Development.json"))
        {
            settings = File.ReadAllText("binance_settings.Development.json");
            var obj2 = JObject.Parse(settings);
            obj.Merge(obj2);
        }
        var apiKey = obj["Key"].ToString();
        var apiSecret = obj["Secret"].ToString();

        exchange = ExchangeBuilder
            .UseBinance()
            .SetCredentials(apiKey, apiSecret)
            .BuildExchange();
    }

    [Test, Order(0)]
    public async Task GetServerTimeTest()
    {
        var dateTime = await exchange.GetServerTime();
        DateTimeOffset def = default;
        Assert.AreNotEqual(dateTime, def);
    }

    [Test, Order(1)]
    public async Task GetBalanceTest()
    {
        var balances = await exchange.GetBalancesAsync();
        Assert.IsNotNull(balances);
    }

    [Test, Order(2)]
    public async Task GetMarketsTest()
    {
        var markets = await exchange.GetMarketsAsync();
        var market = markets.FirstOrDefault(x => x.Base == "BTC" && x.Quote == "USDT");
        Assert.IsNotNull(market);
    }

    [Test, Order(3)]
    public async Task GetTickerTest()
    {
        var ticker = await exchange.GetTickerAsync("BTC", "USDT");
        Assert.IsNotNull(ticker);
    }

    [Test, Order(4)]
    public async Task GetCandlesTest()
    {
        var candles = await exchange.GetCandlesAsync("BTC", "USDT", "1d", 7);
        Assert.IsNotNull(candles);
    }

    [Test, Order(6)]
    public async Task GetOrdersTest()
    {
        var orders = await exchange.GetOrders("BTC", "USDT", 10);
        Assert.IsNotNull(orders);
    }

    [Test, Order(7)]
    public async Task GetOpenOrdersTest()
    {
        var orders = await exchange.GetOpenOrders();
        Assert.IsNotNull(orders);
    }

    [Test, Order(8)]
    public async Task GetTradesTest()
    {
        var trades = await exchange.GetTrades("RVN", "USDT", 10);
        Assert.IsNotNull(trades);
    }

    [Test, Order(9)]
    public async Task PostOrderTest()
    {
        var newId = Guid.NewGuid().ToString();
        var order = new Order
        {
            ClientOrderId = newId,
            Symbol = "BTCUSDT",
            Side = "BUY",
            Type = "LIMIT",
            Price = 35000,
            Quantity = 0.001M,
            TimeInForce = "GTC",
        };
        var newOrder = await exchange.PostOrder(order);
        Assert.IsNotNull(newOrder);
        if (newOrder != null)
        {
            Assert.AreEqual(order.ClientOrderId, newOrder.ClientOrderId);
        }
    }

    [Test, Order(10)]
    public async Task DeleteOrder()
    {
        await PostOrderTest();
        var orders = await exchange.GetOpenOrders();
        foreach (var order in orders)
        {
            await exchange.DeleteOrder(order);
            Assert.AreEqual(order.Status, "CANCELED");
        }
    }
}
