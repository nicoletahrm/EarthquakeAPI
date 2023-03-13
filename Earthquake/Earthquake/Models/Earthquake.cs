using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Earthquake
{
    public class Earthquake
    {
        [BsonId]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
        public Metadata? Metadata { get; set; }
        public List<Feature>? Features { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("generated")]
        public long Generated { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("api")]
        public string? Api { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }

    public class Feature
    {
        public Properties? Properties { get; set; }
        public Geometry? Geometry { get; set; }
    }

    public class Properties
    {
        [JsonProperty("mag")]
        public decimal Magnitude { get; set; }

        [JsonProperty("place")]
        public string? Place { get; set; }

        [JsonProperty("time")]
        public long TimeMs { get; set; }

        public DateTime Time => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(TimeMs);

        [JsonProperty("updated")]
        public object? Updated { get; set; }

        [JsonProperty("tz")]
        public object? Tz { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("detail")]
        public string? Detail { get; set; }

        [JsonProperty("felt")]
        public int? Felt { get; set; }

        [JsonProperty("cdi")]
        public double? Cdi { get; set; }

        [JsonProperty("mmi")]
        public double? Mmi { get; set; }

        [JsonProperty("alert")]
        public string? Alert { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("tsunami")]
        public int Tsunami { get; set; }

        [JsonProperty("sig")]
        public int Sig { get; set; }

        [JsonProperty("net")]
        public string? Net { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("ids")]
        public string? Ids { get; set; }

        [JsonProperty("sources")]
        public string? Sources { get; set; }

        [JsonProperty("types")]
        public string? Types { get; set; }

        [JsonProperty("nst")]
        public int? Nst { get; set; }

        [JsonProperty("dmin")]
        public double? Dmin { get; set; }

        [JsonProperty("rms")]
        public double Rms { get; set; }

        [JsonProperty("gap")]
        public double? Gap { get; set; }

        [JsonProperty("magType")]
        public string? MagType { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("titile")]
        public string? Title { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double>? Coordinates { get; set; }
    }
}
