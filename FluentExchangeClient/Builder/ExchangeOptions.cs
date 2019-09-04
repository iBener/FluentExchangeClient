using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentExchangeClient.Builder
{
    public class ExchangeOptions
    {
        public string ExchangeName { get; internal set; }

        public ApiCredentials Credentials { get; internal set; }

        public HttpClient Http { get; internal set; }

        public IMapper Mapper { get; internal set; }
    }
}
