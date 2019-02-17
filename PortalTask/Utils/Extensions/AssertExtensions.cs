using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace PortalTask.Utils.Extensions
{
    public static class AssertExtensions
    {
        public static void AssertJObjects(this JObject expected, JObject actual, string chackedObjectName)
        {
            foreach (var param in expected)
            {
                Assert.AreEqual(param.Value.ToString(), actual[param.Key].ToString(), $"Check param: {param.Key} from {chackedObjectName}");
            }
        }
    }
}