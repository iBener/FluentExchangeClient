﻿using FluentExchangeClient.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance.Requests;

class BinanceFuturesRequestOrders : BinanceBaseFuturesRequest
{
    public BinanceFuturesRequestOrders(string symbol, string quoteSymbol, DateTime startTime, DateTime endTime, int limit, ExchangeOptions options) :
        base(CreateParamObject(symbol, quoteSymbol, startTime, endTime, options.Timestamp, limit), options)
    {
        Method = HttpMethod.Get;
        RequestUri = new Uri(BaseAddress, "/fapi/v1/allOrders" + QueryString);
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
