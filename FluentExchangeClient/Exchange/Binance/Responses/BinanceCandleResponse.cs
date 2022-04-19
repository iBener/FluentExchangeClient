using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FluentExchangeClient.Exchange.Binance.Responses;

[JsonConverter(typeof(BinanceCandleResponeJsonConverter))]
class BinanceCandleResponse
{
    public long unixTime;
    public DateTimeOffset start;
    public decimal open;
    public decimal high;
    public decimal low;
    public decimal close;
    public decimal volume;
    public decimal quoteVolume;
}

class BinanceCandleResponeJsonConverter : JsonConverter<BinanceCandleResponse>
{
    public override BinanceCandleResponse ReadJson(JsonReader reader, Type objectType, BinanceCandleResponse? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JArray array = JArray.Load(reader);
        return new BinanceCandleResponse
        {
            unixTime = array[0].ToObject<long>(),
            start = DateTimeOffset.FromUnixTimeMilliseconds(array[0].ToObject<long>()),
            open = array[1].ToObject<decimal>(),
            high = array[2].ToObject<decimal>(),
            low = array[3].ToObject<decimal>(),
            close = array[4].ToObject<decimal>(),
            volume = array[5].ToObject<decimal>(),
            quoteVolume = array[7].ToObject<decimal>(),
        };
    }

    public override void WriteJson(JsonWriter writer, BinanceCandleResponse? value, JsonSerializer serializer)
    {
        throw new NotImplementedException("CanWrite is false");
    }

    public override bool CanWrite => false;
}
