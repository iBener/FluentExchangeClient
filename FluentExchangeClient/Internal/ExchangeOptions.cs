using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentExchangeClient.Internal
{
    class ExchangeOptions
    {
        public string ExchangeName { get; set; }

        public ApiCredentials Credentials { get; internal set; }

        public HttpClient Http { get; internal set; }
    }
}
