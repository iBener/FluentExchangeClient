using FluentExchangeClient.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Test;

public class BinancePertpetualTests
{
    IDerivativeExchange binancePerpetual;

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

        binancePerpetual = ExchangeBuilder
            .UseBinance()
            .SetCredentials(apiKey, apiSecret)
            .UseDerivativeExchange()
            .Build();
    }

    [Test]
    public async Task Test1_GetPerpetualMarkets()
    {
        var market = await binancePerpetual.GetMarketAsync("BTC", "USDT");
        Assert.IsNotNull(market);
    }

    [Test]
    public async Task Test2_GetPerpetualCandles()
    {
        var candles = await binancePerpetual.GetCandlesAsync("BTC", "USDT", "1d", 7);
        Assert.IsNotNull(candles);
    }

    [Test]
    public async Task Test3_GetPerpetualTicker()
    {
        var ticker = await binancePerpetual.GetTickerAsync("BTC", "USDT");
        Assert.IsNotNull(ticker);
    }

    [Test]
    public async Task Test4_GetPerpetualBalances()
    {
        var balances = await binancePerpetual.GetBalancesAsync();
        Assert.IsNotNull(balances);
    }

    [Test]
    public async Task Test5_PostOrder()
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
        var newOrder = await binancePerpetual.PostOrder(order);
        Assert.IsNotNull(newOrder);
        if (newOrder != null)
        {
            Assert.AreEqual(order.ClientOrderId, newId);
        }
    }

    [Test]
    public async Task Test6_GetPerpetualOpenOrders()
    {
        var orders = await binancePerpetual.GetOpenOrders();
        Assert.IsNotNull(orders);
    }

    [Test]
    public async Task Test7_DeleteOrder()
    {
        //await Test5_PostOrder();
        var orders = await binancePerpetual.GetOpenOrders();
        foreach (var order in orders)
        {
            var deleted = await binancePerpetual.DeleteOrder(order);
            Assert.AreEqual(deleted.Status, "CANCELED");
        }
    }

    [Test]
    public async Task Test8_GetPerpetualOrders()
    {
        var orders = await binancePerpetual.GetOrders("BTC", "USDT", 10);
        Assert.IsNotNull(orders);
    }

    [Test]
    public async Task Test9_GetPerpetualTrades()
    {
        var trades = await binancePerpetual.GetTrades("BTC", "USDT", 10);
        Assert.IsNotNull(trades);
    }
}
