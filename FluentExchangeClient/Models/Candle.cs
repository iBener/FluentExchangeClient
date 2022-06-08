#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models;

public class Candle
{
    public string Base { get; set; }
    public string Quote { get; set; }
    public DateTimeOffset Start { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public decimal QuoteVolume { get; set; }
}
