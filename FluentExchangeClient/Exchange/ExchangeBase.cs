using AutoMapper;
using FluentExchangeClient.Builder;
using FluentExchangeClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange;

public abstract class ExchangeBase : IDisposable
{
    private readonly HttpClient http;
    private readonly IMapper mapper;

    public ExchangeBase()
    {
    }

    internal ExchangeBase(ExchangeOptions options)
    {
        Options = options;
        http = Options.Http ?? new HttpClient();
        mapper = Options.Mapper ?? throw new ArgumentNullException(nameof(Options.Mapper), "Mapper cannot be null");
    }

    public ExchangeOptions Options { get; }

    public string Name => Options?.ExchangeName;

    protected async Task<T> SendAsync<T>(HttpRequestMessage request)
    {
        string json = await SendAsync(request);
        return JsonConvert.DeserializeObject<T>(json);
    }

    protected async Task<string> SendAsync(HttpRequestMessage request)
    {
        using (request)
        {
            using var response = await http.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return json;
            }
            throw new ExchangeClientException(json);
        }
    }

    internal T Map<T>(object source)
    {
        return mapper.Map<T>(source);
    }

    public void Dispose()
    {
        Debug.WriteLine($"'{Name}' exchange is disposing.");
        if (http != null)
        {
            http.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}
