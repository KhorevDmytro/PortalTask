using NUnit.Framework;

namespace PortalTask.Tests
{
    [Parallelizable]
    public class CommonTests : _BaseTest
    {
        [TestCase("ipsum dolorem", "Marcia@name.biz")]
        public void CheckUserEmailByComment(string partOfComment, string expectedEmail)
        {
            var emails = _systemRequests.GetUserEmailByPartOfComment(partOfComment);
            Assert.Contains(expectedEmail, emails, $"Check that '{partOfComment}' was posted by email: {expectedEmail}");
        }

        [TestCase("eos dolorem iste accusantium est eaque quam", "Patricia Lebsack")]
        public void CheckUserNameByPostedTitle(string title, string expectedUserName)
        {
            var userIds = _systemRequests.GetUserIdByPostTitle(title);
            var userNames = _systemRequests.GetUserNamesByUserIds(userIds);
            Assert.Contains(expectedUserName, userNames, $"Check if user who posted a post with title {title} name is {expectedUserName}");
        }

        [TestCase("ad et natus qui", "Sincere@april.biz")]
        public void CheckThatPhotoTitleBelongsToUserEmail(string title, string expectedEmail)
        {
            var albmIdList = _systemRequests.GetAlbumIdsByPhotoTitle(title);
            var usersIdList = _systemRequests.GetUserIdsByAlbumIds(albmIdList);
            var emails = _systemRequests.GetUserEmailsByUserIds(usersIdList);
            Assert.Contains(expectedEmail, emails, $"Check if photo with title {title} belongs to user with email {expectedEmail}");
        }

        [TestCase("Leanne Graham", "Ervin Howell")]
        public void ChackThatUserDoMoreThenAnother(string firstUserName, string secondUserName)
        {
            var firstUserId = _systemRequests.GetUserIdByName(firstUserName);
            var secondUserId = _systemRequests.GetUserIdByName(secondUserName);
            var completedTaskByFirstUser = _systemRequests.GetComletedTaskByUserId(firstUserId);
            var completedTaskBySecondUser = _systemRequests.GetComletedTaskByUserId(secondUserId);
            Assert.Greater(completedTaskByFirstUser, completedTaskBySecondUser+3, $"check that {firstUserName} has more than 3 'completed' TODOs than {secondUserName}");
        }

        [TestCase(4, "d32776.png")]
        public void ChackThatPhotoIsNotCorrupted(int photoId, string photoName)
        {
            var imageUrl = _systemRequests.GetPhotoUrl(photoId);
            var imageBytesArrayFromUrl = _systemRequests.GetPhotoBytes(imageUrl);
            var imageBytesArrayFromFile = _systemRequests.GetLocalPhotoBytes(photoName);
            Assert.AreEqual(imageBytesArrayFromUrl, imageBytesArrayFromFile, $"Check that image from url isn't corrupted by photoId: {photoId}");
        }
    }
}