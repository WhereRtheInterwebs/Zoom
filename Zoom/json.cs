using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using Zoom.Models;

namespace Zoom
{
    public static class Serialize
    {
        public static string ToJson(this AppSettings self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this MeetingList self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
