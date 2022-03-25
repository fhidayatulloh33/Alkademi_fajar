using Facebooker.Models;

namespace Facebooker.Services
{
    public interface IFeedService
    {
        List<FeedViewModel> GetFeeds();
        int Add(FeedViewModel request);
    }
}