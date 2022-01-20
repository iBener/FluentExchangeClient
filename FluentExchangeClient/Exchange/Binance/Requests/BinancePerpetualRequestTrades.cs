﻿using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinancePerpetualRequestTrades : BinanceBasePerpetualRequest
{
    public BinancePerpetualRequestTrades(string symbol, string quoteSymbol, DateTime startTime, DateTime endTime, long timeStamp, int limit, ApiCredentials credentials) :
        base(CreateParamObject(symbol, quoteSymbol, startTime, endTime, timeStamp, limit), credentials)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/userTrades" + QueryString);
    }

    private static object CreateParamObject(string symbol, string quoteSymbol, DateTime startTime, DateTime endTime, long timestamp, int limit)
    {
        return new
        {
            symbol = symbol + quoteSymbol,
            startTime,
            endTime,
            limit,
            timestamp
        };
    }
}