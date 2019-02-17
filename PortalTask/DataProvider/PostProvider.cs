using System.Collections.Generic;
using PortalTask.DataProvider.Models;

namespace PortalTask.DataProvider
{
    public class PostProvider
    {
        public static IEnumerable<IPostModel> NewPostData
        {
            get
            {
                yield return new PostModel();
            }
        }

        public static IEnumerable<IPostModel> UpdatePostData
        {
            get
            {
                yield return new PostModel()
                {
                    Id = 1,
                    Body = "newAutomationBody",
                    Title = "newAutomationTitle"
                };
            }
        }
    }
}