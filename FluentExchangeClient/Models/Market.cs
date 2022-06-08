#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models;

public class Market
{
    /// <summary>
    /// Base asset name
    /// </summary>
    public string Base { get; set; }

    /// <summary>
    /// Quote asset name
    /// </summary>
    public string Quote { get; set; }

    /// <summary>
    /// Price tick minimum step size
    /// </summary>
    public decimal PriceTick { get; set; }

    /// <summary>
    /// Price tick decimal precision
    /// </summary>
    public int PricePrecision { get; set; }

    /// <summary>
    /// Order amount minimum step size
    /// </summary>
    public decimal OrderTick { get; set; }

    /// <summary>
    /// Order tick decimal precision
    /// </summary>
    public int OrderPrecision { get; set; }

    /// <summary>
    /// Status of market symbol
    /// </summary>
    public string Status { get; set; }
}
