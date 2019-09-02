using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Models
{
    public class ExchangeClientException : Exception
    {
        public ExchangeClientException(string message) : base(message)
        {
        }
    }
}
