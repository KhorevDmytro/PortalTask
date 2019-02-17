using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PortalTask.DataProvider;
using PortalTask.DataProvider.Models;
using PortalTask.Utils.Extensions;

namespace PortalTask.Tests
{
    [Parallelizable]
    public class UpdateTests : _BaseTest
    {
        private JToken _oldPost;

        //Works just for single test. For DDT move it to Test Body
        //Save current postData
        [SetUp]
        public void BeforeTest()
        {
            var post = PostProvider.UpdatePostData;
            _oldPost = _systemRequests.GetPost(post.First().Id);
        }

        [Category("RestTests")]
        [TestCaseSource(typeof(PostProvider), nameof(PostProvider.UpdatePostData))]
        public void UpdatePostTest(IPostModel post)
        {
            var response = _systemRequests.UpdatePost(post);
            var responseFromGet = _systemRequests.GetPost(post.Id);

            Assert.Multiple(() =>
            {
                post.GetJObject().AssertJObjects(response, "response of updating");
                post.GetJObject().AssertJObjects(responseFromGet, "response from GET request");
                Assert.AreEqual(post.GetJObject().Count, response.Count, "Check that qty of params are the same");
            });
        }

        //Returns to previous post data
        [TearDown]
        public void AfterTest()
        {
            _systemRequests.UpdatePost(_oldPost);
        }
    }
}