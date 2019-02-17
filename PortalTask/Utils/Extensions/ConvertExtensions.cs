using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalTask.DataProvider.Models;

namespace PortalTask.Utils.Extensions
{
    public static class ConvertExtensions
    {
        public static JObject GetJObject(this IPostModel postModel)
        {
            return JObject.Parse(JsonConvert.SerializeObject(postModel));
        }
    }
}