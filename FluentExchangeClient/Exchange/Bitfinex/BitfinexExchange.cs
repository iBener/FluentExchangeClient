using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;

namespace FluentExchangeClient.Exchange.Bitfinex
{
    class BitfinexExchange : ExchangeBase //, IExchange
    {
        public BitfinexExchange(ExchangeOptions options) : base(options)
        {
        }
    }
}
