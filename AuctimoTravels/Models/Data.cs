using System.Collections.Generic;
using Newtonsoft.Json;

namespace AuctimoTravels.Models
{
    public class Data
    {
        [JsonProperty("slices")]
        public List<Slice> Slices { get; set; }

        [JsonProperty("passengers")]
        public List<Passenger> Passengers { get; set; }

        [JsonProperty("cabin_class")]
        public string CabinClass { get; set; }
    }
}