using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceBaseFuturesRequest : BinanceBaseRequest
{
    public override Uri BaseAddress { get; }

    public BinanceBaseFuturesRequest(object? param, ExchangeOptions options) : base(param, options)
    {
        if (options.UseTestServer)
        {
            BaseAddress = new("https://testnet.binancefuture.com");
        }
        else
        {
            BaseAddress = new("https://fapi.binance.com");
        }
    }
}
