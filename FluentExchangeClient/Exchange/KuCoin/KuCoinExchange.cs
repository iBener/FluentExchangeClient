using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.KuCoin;

class KuCoinExchange : ExchangeBase //, IExchange
{
    public KuCoinExchange(ExchangeOptions options) : base(options)
    {
    }

    public override Task<string> GetServerTime()
    {
        throw new NotImplementedException();
    }
}
