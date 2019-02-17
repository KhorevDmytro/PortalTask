using System;
using NUnit.Framework;
using PortalTask.DataProvider.Models;

namespace PortalTask.Tests
{
    [Parallelizable]
    public class DeleteTests : _BaseTest
    {
        private int _postId;

        //Pre-condition: add new Post
        [SetUp]
        public void AddPost()
        {
            _postId = _systemRequests.GetLastPostId()+1;
            _systemRequests.AddPost(new PostModel(){Id = _postId});
            if (_systemRequests.GetPost(_postId) == null)
            {
                throw  new Exception("New Post wasn't added");
            }
        }

        [Category("RestTests")]
        [Test]
        public void DeletePostTest()
        {
            _systemRequests.DeletePost(_postId);
            Assert.AreEqual(_systemRequests.GetPost(_postId), null, "Chack that new post isn't existing (was deleted)");
        }
    }
}