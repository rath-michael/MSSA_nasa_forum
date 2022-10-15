using System.Text.Json.Serialization;

namespace Week15Project.Models.News
{
    public class EventType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
