using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace OXamarin.Models
{
    [DataContract]
    [JsonObject("LargeClassification")]
    public class LargeClassification
    {
        [DataMember(Name = "id")]
        [JsonProperty("id")]
        public int id { get; set; }

        [DataMember(Name = "Name")]
        [JsonProperty("Name")]
        public String Name { get; set; }

        [DataMember(Name = "created_at")]
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }

        [DataMember(Name = "updated_at")]
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }
    }

}