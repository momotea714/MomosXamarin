using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace OXamarin.Models
{
    [DataContract]
    [JsonObject("favorite")]
    public class Favorite
    {
        [DataMember(Name = "id")]
        [JsonProperty("id")]
        public int id { get; set; }

        [DataMember(Name = "categoryCD")]
        [JsonProperty("categoryCD")]
        public int categoryCD { get; set; }

        [DataMember(Name = "title")]
        [JsonProperty("title")]
        public String title { get; set; }

        [DataMember(Name = "content")]
        [JsonProperty("content")]
        public String content { get; set; }

        [DataMember(Name = "created_at")]
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }

        [DataMember(Name = "updated_at")]
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }
    }

public class NewModel
{
	public List<Favorite> favorite { get; set; }
}
}