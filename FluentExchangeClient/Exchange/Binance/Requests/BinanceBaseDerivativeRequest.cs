using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceBaseDerivativeRequest : BinanceBaseRequest
{
    public override Uri BaseAddress => new("https://fapi.binance.com");

    public BinanceBaseDerivativeRequest(object param, ApiCredentials credentials) : base(param, credentials)
    {
    }
}
