using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models
{
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
        /// Min. price precision
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// Min. order step size
        /// </summary>
        public decimal Step { get; set; }
    }
}
