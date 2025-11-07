using Newtonsoft.Json;

namespace Backend.Domain.DTOs.Requests
{
    public class PushNotificationModel
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Badge { get; set; }
        public string Priority { get; set; }
        public string Sound { get; set; }
    }

    public class NotificationModel
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
        [JsonProperty("isAndroiodDevice")]
        public bool IsAndroiodDevice { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("senderId")]
        public string SenderId { get; set; }
        [JsonProperty("serverKey")]
        public string ServerKey { get; set; }
    }

    public class GoogleNotification
    {
        public class DataPayload
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }
        }
        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";
        [JsonProperty("data")]
        public DataPayload Data { get; set; }
        [JsonProperty("notification")]
        public DataPayload Notification { get; set; }
    }
}
