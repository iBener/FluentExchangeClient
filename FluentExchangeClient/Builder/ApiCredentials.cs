using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FluentExchangeClient.Builder
{
    public class ApiCredentials
    {
        public string ApiKey { get; internal set; }

        public HMAC Hash { get; internal set; }

        public string Sign(string s)
        {
            var messageBytes = Encoding.UTF8.GetBytes(s);
            var computedHash = Hash.ComputeHash(messageBytes);
            return BitConverter.ToString(computedHash).Replace("-", "").ToLower();
        }
    }
}
