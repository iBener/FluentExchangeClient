using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models
{
    public class Candle
    {
        public DateTimeOffset Start { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public decimal BaseVolume { get; set; }
    }
}
