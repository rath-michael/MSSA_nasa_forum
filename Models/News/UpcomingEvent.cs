using System.Text.Json.Serialization;

namespace Week15Project.Models.News
{
    public class UpcomingEvent
    {
        [JsonPropertyName("next")]
        public string NextResult { get; set; }

        [JsonPropertyName("results")]
        public List<EventResult> Results { get; set; }
    }
}
