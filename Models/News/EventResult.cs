using System.Text.Json.Serialization;

namespace Week15Project.Models.News
{
    public class EventResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string EventURL { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public EventType EventType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("webcast_live")]
        public bool LiveWebcast { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("news_url")]
        public string NewsURL { get; set; }

        [JsonPropertyName("video_url")]
        public string VideoURL { get; set; }

        [JsonPropertyName("feature_image")]
        public string Image { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        public int? postId { get; set; }

        public int RoomId { get; set; }
    }
}
