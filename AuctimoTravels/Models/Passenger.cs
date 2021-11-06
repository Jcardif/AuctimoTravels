using Newtonsoft.Json;

namespace AuctimoTravels.Models
{
    public class Passenger
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}