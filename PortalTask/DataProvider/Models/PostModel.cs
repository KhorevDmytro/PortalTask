using Newtonsoft.Json;

namespace PortalTask.DataProvider.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PostModel : IPostModel
    {
        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; } = 211;

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; } = null;

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; } = "AutomationTitle";

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; } = "AutomationBody";
    }
}