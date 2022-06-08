using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;

namespace FluentExchangeClient.Exchange.Bitfinex
{
    public class BitfinexExchangeBase : ExchangeBase
    {
        public BitfinexExchangeBase(ExchangeOptions options) : base(options)
        {
        }

        public override Task<string> GetServerTime()
        {
            throw new NotImplementedException();
        }
    }
}
