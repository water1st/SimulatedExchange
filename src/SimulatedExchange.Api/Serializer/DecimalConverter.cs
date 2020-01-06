using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimulatedExchange.Api.Serializer
{
    public class DecimalConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            decimal result = 0;

            var value = reader.GetString();
            decimal.TryParse(value, out result);

            return result;
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

        public static DecimalConverter CreateInstance() => new DecimalConverter();
    }
}
