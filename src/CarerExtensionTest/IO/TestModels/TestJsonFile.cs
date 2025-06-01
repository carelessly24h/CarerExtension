namespace CarerExtensionTest.IO.TestModels;

internal class TestJsonFile : JsonIO<TestJsonFile>
{
    [JsonPropertyName("intItem")]
    public int? IntValue { get; set; }

    [JsonPropertyName("doubleItem")]
    public double? DoubleValue { get; set; }

    [JsonPropertyName("stringItem")]
    public string? StringValue { get; set; }

    [JsonPropertyName("dateItem")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? DateTimeValue { get; set; }

    [JsonPropertyName("boolItem")]
    public bool? BoolValue { get; set; }

    [JsonPropertyName("dictionaryItem")]
    public Dictionary<string, string> DictionaryValue { get; set; } = [];

    [JsonIgnore]
    public int NoTarget1 = 1;

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private const string FORMAT = "yyyyMMddHHmmss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetString() switch
            {
                string v => v.ToDateTimeOrDefault(FORMAT) ?? DateTime.MinValue,
                _ => DateTime.MinValue,
            };

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(FORMAT));
    }
}
