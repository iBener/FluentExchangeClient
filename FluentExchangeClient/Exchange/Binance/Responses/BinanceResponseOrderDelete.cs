#pragma warning disable CS0649
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FluentExchangeClient.Exchange.Binance.Responses;

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
