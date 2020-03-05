using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance
{
    public abstract class BinanceExchangeBase : ExchangeBase
    {
        internal BinanceExchangeBase(ExchangeOptions options) : base(options)
        {
        }

        private static double? serverTimeDiff = null;

        public long Timestamp
        {
            get
            {
                double diff;
                if (serverTimeDiff == null)
                {
                    var result = GetServerTime().GetAwaiter().GetResult();
                    var time = JsonConvert.DeserializeObject<BinanceServerTimeResponse>(result);
                    var serverTime = DateTimeOffset.FromUnixTimeMilliseconds(time.serverTime);
                    serverTimeDiff = (serverTime - DateTimeOffset.UtcNow).TotalMilliseconds;
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

        public abstract Task<string> GetServerTime();
    }
}
