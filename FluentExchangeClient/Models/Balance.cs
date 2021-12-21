using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models;

public class Balance
{
    public string Symbol { get; set; }
    public decimal Amount { get; set; }
    public decimal Locked { get; set; }
    public decimal Free { get; set; }
}
