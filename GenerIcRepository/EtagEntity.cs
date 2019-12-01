using Newtonsoft.Json;
using System;

namespace GenerIcRepository
{
    public class EtagEntity 
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "eTag")]
        public string ETag { get; set; }
    }
}
