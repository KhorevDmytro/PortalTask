using System;
using System.Net.Http;
using System.Text;

namespace PortalTask.Utils
{
    public class ApiClient
    {
        public HttpClient _client;

        public ApiClient(string domain)
        {
            _client = new HttpClient { BaseAddress = new Uri(domain) };
        }

        public HttpResponseMessage Get(string uri)
        {
            return _client.GetAsync(uri).GetAwaiter().GetResult();
        }

        public HttpResponseMessage Post(string uri, string jsonObject)
        {
            var content = GetContent(jsonObject);
            return _client.PostAsync(uri, content).GetAwaiter().GetResult();
        }

        public HttpResponseMessage Patch(string uri, int postId, string jsonObject)
        {
            return Request("PATCH", $"{uri}/{postId.ToString()}", jsonObject);
        }

        public HttpResponseMessage Delete(string uri, int postId)
        {
            return _client.DeleteAsync($"{uri}/{postId.ToString()}").GetAwaiter().GetResult();
        }

        private HttpContent GetContent(string jsonObject)
        {
            return new StringContent(jsonObject, Encoding.UTF8, "application/json");
        }

        //Universal Method
        private HttpResponseMessage Request(string methodName, string uri, string jsonObject)
        {
            var method = new HttpMethod(methodName);
            var request = new HttpRequestMessage(method, uri)
            {
                Content = GetContent(jsonObject)
            };
            return _client.SendAsync(request).GetAwaiter().GetResult();
        }
    }
}