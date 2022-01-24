using FluentExchangeClient.Exchange.Binance;
using FluentExchangeClient.Exchange.Bitfinex;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Test;

public class BuilderTests
{
    [Test]
    public void Test1_BinanceExchangeBuildTest()
    {
        var exchange = ExchangeBuilder
            .UseBinance()
            .Build();
        Assert.IsAssignableFrom<BinanceExchange>(exchange);
    }

    [Test]
    public void Test2_BinanceExchangeRawBuildTest()
    {
        var exchange = ExchangeBuilder
            .UseBinance()
            .UseRawExchange()
            .Build();
        Assert.IsAssignableFrom<BinanceExchangeRaw>(exchange);
    }

    [Test]
    public void Test3_BinanceDerivativeExchangeTest()
    {
        var exchange = ExchangeBuilder
            .UseBinance()
            .UseDerivativeExchange()
            .Build();
        Assert.IsAssignableFrom<BinanceDerivativeExchange>(exchange);
    }

    [Test]
    public void Test4_BinanceDerivativeExchangeRawTest()
    {
        var exchange = ExchangeBuilder
            .UseBinance()
            .UseRawDerivativeExchange()
            .Build();
        Assert.IsAssignableFrom<BinanceDerivativeExchangeRaw>(exchange);
    }

    [Test]
    public void Test2_BitfinexExchangeBuild()
    {
        var exchange = ExchangeBuilder
            .UseBitfinex()
            .Build();
        Assert.IsAssignableFrom<BitfinexExchange>(exchange);
    }
}
