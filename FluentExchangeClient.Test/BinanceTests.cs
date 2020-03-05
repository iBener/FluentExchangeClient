using FluentExchangeClient.Builder;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FluentExchangeClient.Test
{
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

            exchange = new ExchangeBuilder()
                .UseBinance()
                .SetCredentials(apiKey, apiSecret)
                .Build();
        }

        [Test]
        public async Task GetBalanceTest()
        {
            var balances = await exchange.GetBalancesAsync();
            Assert.IsNotNull(balances);
        }

        [Test]
        public async Task GetMarketsTest()
        {
            var markets = await exchange.GetMarketsAsync();
            Assert.IsNotNull(markets);
        }

        [Test]
        public async Task GetTickerTest()
        {
            var ticker = await exchange.GetTickerAsync("BTC", "USDT");
            Assert.IsNotNull(ticker);
        }

        [Test]
        public async Task GetServerTimeTest()
        {
            var dateTime = await exchange.GetServerTime();
            DateTimeOffset def = default;
            Assert.AreNotEqual(dateTime, def);
        }

        [Test]
        public async Task GetCandlesTest()
        {
            var candles = await exchange.GetCandlesAsync("BTC", "USDT", "1d", 7);
            Assert.IsNotNull(candles);
        }

        [Test]
        public async Task GetAllCandlesTest()
        {
            var candles = await exchange.GetAllCandlesAsync("BTC", "1d", 1);
            Assert.IsNotNull(candles);
        }
    }
}