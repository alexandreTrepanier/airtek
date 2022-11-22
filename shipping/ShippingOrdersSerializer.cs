using System.Text.Json;
using System.Text.Json.Serialization;
using airtek.flight;

namespace airtek.shipping;

public class ShippingOrdersSerializer
{
    public readonly struct JsonShippingOrder
    {
        [JsonPropertyName("destination")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IAtaCode Destination { get; init; }
    }

    protected JsonSerializerOptions _options = new();

    protected virtual async Task<Dictionary<string, JsonShippingOrder>> DeserializeAsync(string fileName, CancellationToken cancelTkn = default)
    {
        using var json = File.OpenRead(fileName);

        if (json == null)
            throw new Exception(@$"Problem reading {fileName}");

        return await JsonSerializer.DeserializeAsync<Dictionary<string, JsonShippingOrder>>(json!, _options, cancelTkn) ?? new Dictionary<string, JsonShippingOrder>();
    }

    public virtual async Task<List<ShippingOrder>> DeserializeAsync(string fileName, IAtaCode defaultDeparture, CancellationToken cancelTkn = default)
    {
        return (await DeserializeAsync(fileName, cancelTkn))
               .Select((o, i) => new ShippingOrder
               {
                   Id = o.Key,
                   OrderNumber = i,
                   Departure = defaultDeparture,
                   Destination = o.Value.Destination
               }).ToList();
    }
}
