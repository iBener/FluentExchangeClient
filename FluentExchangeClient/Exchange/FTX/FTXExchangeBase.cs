using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.FTX
{
    public class FTXExchangeBase : ExchangeBase
    {
        public FTXExchangeBase(ExchangeOptions options) : base(options)
        {
        }

        public override Task<string> GetServerTime()
        {
            throw new NotImplementedException();
        }
    }
}
