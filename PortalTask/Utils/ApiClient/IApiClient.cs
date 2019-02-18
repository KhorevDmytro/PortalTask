using System.Net.Http;

namespace PortalTask.Utils.ApiClient
{
    public interface IApiClient
    {
        HttpResponseMessage Get(string uri);

        HttpResponseMessage Post(string uri, string jsonObject);

        HttpResponseMessage Patch(string uri, int postId, string jsonObject);

        HttpResponseMessage Delete(string uri, int postId);
    }
}