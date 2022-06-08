using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FluentExchangeClient.Exchange;

abstract class ExchangeRequestBase : HttpRequestMessage
{
    public abstract Uri BaseAddress { get; }

    public NameValueCollection Query { get; }

    public string QueryString => Query.Count > 0 ? $"?{Query}" : "";

    public ExchangeRequestBase()
    {
        Query = HttpUtility.ParseQueryString(String.Empty);
    }

    public string Sign(HMAC hmac)
    {
        string? query = Query.ToString();
        if (!String.IsNullOrEmpty(query))
        {
            var messageBytes = Encoding.UTF8.GetBytes(query);
            var computedHash = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(computedHash).Replace("-", "").ToLower();
        }
        return String.Empty;
    }
}
