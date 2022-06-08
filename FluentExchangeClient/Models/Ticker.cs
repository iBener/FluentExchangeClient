#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
