using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Models;

public class Trade
{
    public Trade()
    {
        Commissions = new Dictionary<string, decimal>();
        Transactions = new List<TradeTransaction>();
    }

    public string Id { get; set; }

    public int OrderId { get; set; }

    public string Symbol { get; set; }

    public decimal Price { get; set; }

    public decimal Quantity { get; set; }

    public decimal QuoteQuantity { get; set; }

    public DateTime Time { get; set; }

    public string Side { get; set; }

    public IDictionary<string, decimal> Commissions { get; set; }

    public IEnumerable<TradeTransaction> Transactions { get; set; }
}

public class TradeTransaction
{
    public DateTime Time { get; set; }
    public decimal Quantity { get; set; }
    public decimal QuoteQuantity { get; set; }
}
