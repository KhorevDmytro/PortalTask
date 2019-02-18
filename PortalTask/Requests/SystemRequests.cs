using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalTask.Common;
using PortalTask.DataProvider.Models;
using PortalTask.Utils.ApiClient;
using PortalTask.Utils.Extensions;

namespace PortalTask.Requests
{
    public class SystemRequests
    {
        private IApiClient _apiClient;

        public SystemRequests()
        {
            _apiClient = new ApiClient(UriConst.Domain);
        }

        public JObject GetPost(int? id)
        {
            var posts = GetAllPosts();
            return (JObject) posts.FirstOrDefault(p => (int) p["id"] == id);
        }

        //Add Post and Verify success status code
        public JObject AddPost(IPostModel post)
        {
            var context = _apiClient.Post(UriConst.Posts, JsonConvert.SerializeObject(post));
            return context.VerifyCode().GetJObject();
        }

        public int GetLastPostId()
        {
            var allPosts = GetAllPosts();
            return (int) allPosts.Last["id"];
        }

        //Update Post and Verify success status code
        public JObject UpdatePost(dynamic post)
        {
            HttpResponseMessage context = _apiClient.Patch(UriConst.Posts, post.Id, JsonConvert.SerializeObject(post));
            return context.VerifyCode().GetJObject();
        }

        //Update Post and Verify success status code
        public JObject DeletePost(int postId)
        {
            HttpResponseMessage context = _apiClient.Delete(UriConst.Posts, postId);
            return context.VerifyCode().GetJObject();
        }

        public List<string> GetUserEmailByPartOfComment(string partOfComment)
        {
            return GetAllComments().Where(c => c["body"].ToString().Contains(partOfComment)).Select(e => e["email"].ToString()).ToList();
        }

        public List<int> GetUserIdByPostTitle(string title)
        {
            return GetAllPosts().Where(t => t["title"].ToString() == title).Select(e => (int) e["userId"]).ToList();
        }

        public List<int> GetAlbumIdsByPhotoTitle(string title)
        {
            return GetAllPhotos().Where(t => t["title"].ToString() == title).Select(e => (int) e["albumId"]).ToList();
        }

        public List<string> GetUserNamesByUserIds(List<int> ids)
        {
            return ids.Select(x => GetUserNameByUserId(x)).ToList();
        }

        public List<int> GetUserIdsByAlbumIds(List<int> albumIds)
        {
            return albumIds.Select(x => (int) GetAllAlbums().First(a => (int)a["id"] == x)["userId"]).ToList();
        }

        public List<string> GetUserEmailsByUserIds(List<int> userIds)
        {
            return userIds.Select(x => GetUserDataByUserId(x, "email")).ToList();
        }

        public int GetComletedTaskByUserId(int userId)
        {
            return GetAllTodos()
                .Where(x => (int) x["userId"] == userId)
                .Count(t => (bool) t["completed"] == true);
        }

        public int GetUserIdByName(string userName)
        {
            return (int) GetAllUsers().FirstOrDefault(u => u["name"].ToString() == userName)["id"];
        }

        public string GetPhotoUrl(int photoId)
        {
            return GetAllPhotos().First(p => (int) p["id"] == photoId)["url"].ToString();
        }

        public byte[] GetPhotoBytes(string url)
        {
            using (var client = new HttpClient())
            {
                using (var result = client.GetAsync(url).GetAwaiter().GetResult())
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return result.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                    }

                }
            }
            return null;
        }

        public byte[] GetLocalPhotoBytes(string fileName)
        {
            return File.ReadAllBytes($"{Directory.GetCurrentDirectory()}/DataProvider/Resources/{fileName}");
        }

        private string GetUserNameByUserId(int userId)
        {
            return GetUserDataByUserId(userId, "name");
        }

        private string GetUserDataByUserId(int userId, string param)
        {
            return GetAllUsers().First(u => (int) u["id"] == userId)[param].ToString();
        }

        private JArray GetAllPhotos()
        {
            return GetAllResponse(UriConst.Photos);
        }

        private JArray GetAllAlbums()
        {
            return GetAllResponse(UriConst.Albums);
        }

        private JArray GetAllPosts()
        {
            return GetAllResponse(UriConst.Posts);
        }

        private JArray GetAllTodos()
        {
            return GetAllResponse(UriConst.Todos);
        }

        private JArray GetAllComments()
        {
            return GetAllResponse(UriConst.Comments);
        }

        private JArray GetAllUsers()
        {
            return GetAllResponse(UriConst.Users);
        }

        private JArray GetAllResponse(string uri)
        {
            var context = _apiClient.Get(uri);
            return context.VerifyCode().GetJArray();
        }
    }
}