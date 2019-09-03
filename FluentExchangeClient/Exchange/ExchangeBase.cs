using FluentExchangeClient.Internal;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Internal
{
    abstract class ExchangeBase : IDisposable
    {
        private readonly HttpClient http;

        internal ExchangeBase(ExchangeOptions options)
        {
            Options = options;
            http = Options.Http??new HttpClient();
        }

        public ExchangeOptions Options { get; }

        protected async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            string json = await SendAsync(request);
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected async Task<string> SendAsync(HttpRequestMessage request)
        {
            try
            {
                using (request)
                {
                    using (var response = await http.SendAsync(request))
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            return json;
                        }
                        throw new ExchangeClientException(json);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            if (Options.Http == null)
            {
                http.Dispose();
            }
        }
    }
}
