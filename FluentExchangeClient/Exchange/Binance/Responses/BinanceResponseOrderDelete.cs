#pragma warning disable CS0649

using FluentExchangeClient.Mapper;
using Newtonsoft.Json;
using System;

namespace FluentExchangeClient.Exchange.Binance.Responses
{
    class BinanceResponseOrderDelete
    {
        public string symbol;
        public long orderId;
        public string clientOrderId;
        public string origClientOrderId;
        public decimal price;
        public decimal origQty;
        public decimal executedQty;
        public decimal cummulativeQuoteQty;
        public string status;
        public string side;
        public string timeInForce;
        public string type;
    }
}
