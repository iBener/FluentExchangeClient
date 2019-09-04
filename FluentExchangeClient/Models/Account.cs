using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models
{
    public class Account
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Exchange { get; set; }
    }
}
