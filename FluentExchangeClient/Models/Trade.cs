using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Models
{
    public class Trade
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public string Base { get; set; }

        public string Quote { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal QuoteQuantity { get; set; }

        public DateTime Time { get; set; }

        public string Side { get; set; }

        public IDictionary<string, decimal> Commissions { get; set; }
    }
}