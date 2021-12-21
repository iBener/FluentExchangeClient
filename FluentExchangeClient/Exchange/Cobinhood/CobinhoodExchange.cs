using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Cobinhood;

class CobinhoodExchange : ExchangeBase //, IExchange
{
    public CobinhoodExchange(ExchangeOptions options) : base(options)
    {
    }
}
