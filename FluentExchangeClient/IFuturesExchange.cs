using FluentExchangeClient.Common;
using FluentExchangeClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient;

public interface IFuturesExchange : IExchange, IFuturesExchangeRaw
{
    new Task<Leverage?> ChangeLeverage(string symbol, int leverage);

    new Task<Response?> ChangeMarginTypeAsync(string symbol, string marginType);

    new Task<Response?> ChangePositionMarginAsync(string symbol, decimal amount, ChangePositionMargin type);
}
