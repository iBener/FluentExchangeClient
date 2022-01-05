using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Poloniex;

class PoloniexExchange : ExchangeBase //, IExchange
{
    public PoloniexExchange(ExchangeOptions options) : base(options)
    {
    }

    public override Task<string> GetServerTime()
    {
        throw new NotImplementedException();
    }
}
