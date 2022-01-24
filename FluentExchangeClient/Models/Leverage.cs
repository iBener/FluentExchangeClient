using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Models;

public class Leverage
{
    public string Symbol { get; set; }
    public int InitialLeverage { get; set; }
    public decimal MaxNotionalValue { get; set; }
}
