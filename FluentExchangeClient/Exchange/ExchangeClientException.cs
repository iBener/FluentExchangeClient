using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExchangeClient.Exchange;

public class ExchangeClientException : Exception
{
    public ExchangeClientException(string message) : base(message)
    {
    }
}
