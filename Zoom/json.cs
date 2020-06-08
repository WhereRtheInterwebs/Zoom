using Newtonsoft.Json;
using Zoom.Models;

namespace Zoom
{
    public static class Serialize
    {
        public static string ToJson(this MeetingList self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this User self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                //new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
