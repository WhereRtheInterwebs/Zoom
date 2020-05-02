using Newtonsoft.Json;
using System;

namespace Zoom.Models
{
    public partial class AppSettings
    {
        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }

        [JsonProperty("ApiSecret")]
        public string ApiSecret { get; set; }
    }

    public partial class AppSettings
    {
        public static AppSettings FromJson(string json) => JsonConvert.DeserializeObject<AppSettings>(json, Converter.Settings);
    }
}