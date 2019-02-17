namespace PortalTask.DataProvider.Models
{
    public interface IPostModel
    {
        int UserId { get; }

        int? Id { get; set; }

        string Title { get; set; }

        string Body { get; set; }
    }
}
