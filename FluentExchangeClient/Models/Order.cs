using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Models
{
    public class Order
    {
        public string OrderId { get; set; }

        public string ClientOrderId { get; set; }

        public string Symbol { get; set; }

        public string Side { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal FilledQuantity { get; set; }

        public decimal QuoteQuantity { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public DateTime TransactionTime { get; set; }

        public string TimeInForce { get; set; }
    }
}