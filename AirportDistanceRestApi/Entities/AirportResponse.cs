using System.Text.Json.Serialization;

namespace AirportDistanceRestApi.Entities
{
    public class AirportResponse
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; } = null!;

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; } = null!;

        public double LatitudeDouble => double.Parse(Latitude.Substring(0, 2));

        public double LongitudeDouble => double.Parse(Longitude.Substring(0, 2));
    }
}
