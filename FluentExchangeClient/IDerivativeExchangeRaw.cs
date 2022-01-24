using FluentExchangeClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient;

public interface IFuturesExchangeRaw : IExchangeRaw
{
    Task<string> ChangeLeverage(string symbol, int leverage);

    Task<string> ChangeMarginTypeAsync(string symbol, string marginType);

    Task<string> ChangePositionMarginAsync(string symbol, decimal amount, ChangePositionMargin type);
}
