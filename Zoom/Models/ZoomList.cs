using Newtonsoft.Json;

namespace Zoom.Models
{
    public abstract class ZoomList
    {
        [JsonProperty("page_count")]
        public long PageCount { get; set; }

        [JsonProperty("page_number")]
        public long PageNumber { get; set; }

        [JsonProperty("page_size")]
        public long PageSize { get; set; }

        [JsonProperty("total_records")]
        public long TotalRecords { get; set; }
    }
}