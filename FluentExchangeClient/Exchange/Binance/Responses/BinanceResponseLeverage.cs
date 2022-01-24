using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Responses;

public class BinanceResponseLeverage
{
    public string symbol;
    public int leverage;
    public decimal maxNotionalValue;
}
