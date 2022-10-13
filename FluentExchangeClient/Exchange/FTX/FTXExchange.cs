using FluentExchangeClient.Builder;
using FluentExchangeClient.Exchange.Binance;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.FTX;

class FTXExchange : ExchangeBase //, IExchange
{
    public FTXExchange(ExchangeOptions options) : base(options)
    {
    }

    public override Task<string> GetServerTime()
    {
        throw new NotImplementedException();
    }
}
