using System.Text.Json.Serialization;

namespace Week15Project.Models.News
{
    public class NASA_POTD
    {
        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("date")]
        public string DateString { get; set; }
        public DateTime DatePosted { get; set; }

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; }

        [JsonPropertyName("hdurl")]
        public string Hdurl { get; set; }

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }

        [JsonPropertyName("service_version")]
        public string ServiceVersion { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
