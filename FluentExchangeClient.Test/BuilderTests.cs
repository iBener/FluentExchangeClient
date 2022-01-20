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
    public void Test1_BinanceExchangeBuild()
    {
        var exchange = ExchangeBuilder
            .UseBinance()
            .Build();
        Assert.IsAssignableFrom<BinanceExchange>(exchange);
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
