using FluentExchangeClient.Exchange.Binance;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Test;

public class OtherTests
{

    //[Test]
    //public async Task Test01_RequestLimiter()
    //{
    //    int availableWeight = 2400;
    //    int usedWeight = 0;
    //    int limit = 1200;
    //    int weight = 5;
    //    var limiter = new BinanceRequestLimiter(limit);
    //    var sw = Stopwatch.StartNew();
    //    while (usedWeight + weight <= availableWeight)
    //    {
    //        await limiter.WaitRequestLimit(weight);
    //        await Task.Delay(50);
    //        Assert.GreaterOrEqual(limit, limiter.UsedLimit);
    //        usedWeight += weight;
    //    }
    //    sw.Stop();
    //}
}
