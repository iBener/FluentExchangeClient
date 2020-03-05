using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Mapper
{
    public class UnixMillisecondsTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, [AllowNull] DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long.TryParse(reader.Value?.ToString(), out long time);
            return DateTimeOffset.FromUnixTimeMilliseconds(time);
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] DateTimeOffset value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
