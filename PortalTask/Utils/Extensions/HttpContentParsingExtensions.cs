using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace PortalTask.Utils.Extensions
{
    public static class HttpContentParsingExtensions
    {
        public static JArray GetJArray(this HttpResponseMessage content)
        {
            return JArray.Parse(ReadContent(content));
        }

        public static JObject GetJObject(this HttpResponseMessage content)
        {
            return JObject.Parse(ReadContent(content));
        }

        private static string ReadContent(HttpResponseMessage content)
        {
            return content.Content.ReadAsStringAsync().Result;;
        }
    }
}