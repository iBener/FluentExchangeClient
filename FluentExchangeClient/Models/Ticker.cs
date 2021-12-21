using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models;

public class Ticker
{
    public string Pair { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
}
