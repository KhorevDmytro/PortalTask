using NUnit.Framework;
using PortalTask.DataProvider;
using PortalTask.DataProvider.Models;

namespace PortalTask.Tests
{
    //NonParallelizable because it can effect "delete" test
    [NonParallelizable]
    public class AddTests : _BaseTest
    {
        private int _newPostId;

        [Category("RestTests")]
        [TestCaseSource(typeof(PostProvider), nameof(PostProvider.NewPostData))]
        public void AddPostTest(IPostModel post)
        {
            var _newPostId = _systemRequests.GetLastPostId()+1;
            var response = _systemRequests.AddPost(post);

            //Can assert all json by one assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(post.Title, response["title"].ToString(), "Check response param: title");
                Assert.AreEqual(post.UserId, (int)response["userId"], "Check response param: userId");
                Assert.AreEqual(post.Body, response["body"].ToString(), "Check response param: body");
                Assert.AreEqual(_newPostId, (int)response["id"], "Check response param: id");
            });
            //I've skipped all new post param verification by get request for test task. Check just that new post is returned by GET request.
            Assert.AreEqual(_newPostId, _systemRequests.GetLastPostId(), "Check that new post is returned by GET request");
        }

        //Post-condition: delete new post
        [TearDown]
        public void DeleteNewPost()
        {
            _systemRequests.DeletePost(_newPostId);
        }
    }
}