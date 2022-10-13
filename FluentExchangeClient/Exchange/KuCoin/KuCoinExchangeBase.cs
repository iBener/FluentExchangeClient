using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.KuCoin
{
    public class KuCoinExchangeBase : ExchangeBase
    {
        public KuCoinExchangeBase(ExchangeOptions options) : base(options)
        {
        }

        public override Task<string> GetServerTime()
        {
            throw new NotImplementedException();
        }
    }
}
