# Usage
```c#
// Create Binance exchange client
var exchange = ExchangeBuilder
    .UseBinance()
    .SetCredentials(apiKey, apiSecret)
    .Build();

// Get daily BTCUSDT candles for the last 7 days
var candles = await exchange.GetCandlesAsync("BTC", "USDT", interval: "1d", limit: 7);
foreach (var candle in candles)
{
    Console.WriteLine($"Symbol: {candle.Base}{candle.Quote}, Date: {candle.Start:d}, Close: {candle.Close}");
}

// Create new order
var lastCandle = candles.Last();
var newOrder = new Order
{
    ClientOrderId = Guid.NewGuid().ToString(),
    Symbol = "BTCUSDT",
    Side = "BUY",
    Type = "LIMIT",
    Price = lastCandle.Close - lastCandle.Close * 0.15m,
    Quantity = 0.001m,
};

// Post the order
var actualOrder = await exchange.PostOrder(newOrder);
Assert.AreEqual(newOrder.ClientOrderId, actualOrder.ClientOrderId);

// Cancel the order
var deletedOrder = await exchange.DeleteOrder(actualOrder);
Assert.AreEqual("CANCELED", deletedOrder.Status);
```



# Futures / Perpetual Exchange
```c#
var binancePerpetual = ExchangeBuilder
    .UseBinance()
    .SetCredentials(apiKey, apiSecret)
    .UseFuturesExchange()
    .Build();
var candles = await binancePerpetual.GetCandlesAsync("BTC", "USDT", interval: "1d", limit: 7);
```
