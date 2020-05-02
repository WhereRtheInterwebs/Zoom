using Newtonsoft.Json;

namespace Zoom.Models
{
    public partial class AppSettings
    {
        public int LastKeyIndexUsed { get; set; }
        public Keys[] Keys { get; set; }
    }

    public class Keys
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

    public partial class AppSettings
    {
        public static AppSettings FromJson(string json) => JsonConvert.DeserializeObject<AppSettings>(json, Converter.Settings);
    }
}