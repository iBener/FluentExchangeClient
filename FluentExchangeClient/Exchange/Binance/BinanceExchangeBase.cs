using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance
{
    abstract class BinanceExchangeBase : ExchangeBase
    {
        internal BinanceExchangeBase(ExchangeOptions options) : base(options)
        {
        }

        private DateTimeOffset serverTime;

        private static double? serverTimeDiff = null;

        protected long Timestamp
        {
            get
            {
                double diff = 0;
                if (serverTimeDiff == null)
                {
                    var result = GetServerTime().Result;
                    if (Int64.TryParse(result, out long unixTime))
                    {
                        serverTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
                        serverTimeDiff = (serverTime - DateTimeOffset.UtcNow).TotalMilliseconds;
                        diff = serverTimeDiff.Value;
                    }
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
