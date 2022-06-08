#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
