using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Bittrex;

class BittrexExchange : ExchangeBase //, IExchange
{
    public BittrexExchange(ExchangeOptions options) : base(options)
    {
    }
}
