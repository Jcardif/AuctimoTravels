using Newtonsoft.Json;

namespace AuctimoTravels.Models
{
    public class OfferRequestPayload
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
