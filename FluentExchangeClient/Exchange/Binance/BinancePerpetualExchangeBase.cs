using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Responses;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

public abstract class BinancePerpetualExchangeBase : ExchangeBase
{
    internal BinancePerpetualExchangeBase(ExchangeOptions options) : base(options)
    {
    }

    private static double? serverTimeDiff = null;

    public override long Timestamp
    {
        get
        {
            double diff;
            if (serverTimeDiff == null)
            {
                var result = GetServerTime().GetAwaiter().GetResult();
                var response = JsonConvert.DeserializeObject<BinanceResponseServerTime>(result);
                serverTimeDiff = (response.serverTime - DateTimeOffset.UtcNow).TotalMilliseconds;
                diff = serverTimeDiff.Value;
            }
            else
            {
                diff = serverTimeDiff.Value;
            }
            var now = DateTimeOffset.UtcNow.AddMilliseconds(diff);
            return now.ToUnixTimeMilliseconds();
        }
    }

    public override Task<string> GetServerTime()
        => throw new NotImplementedException();

}
