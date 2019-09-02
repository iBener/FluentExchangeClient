using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Exchange.Binance
{
    class BinanceExchange : ExchangeBase, IExchange
    {
        public BinanceExchange(ExchangeOptions options) : base(options)
        {
        }


    }
}
