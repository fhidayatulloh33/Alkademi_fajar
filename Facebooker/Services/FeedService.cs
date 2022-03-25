using Facebooker.Models;

namespace Facebooker.Services
{
    public class FeedService : IFeedService
    {
        List<Feed> _feeds;
        public FeedService()
        {
            _feeds = new List<Feed>
            {
                new Feed("Title", "Bootcamp alkademi"),
                new Feed("Title 1", "Bootcamp alkademi"),

            };
        }
        public int Add(FeedViewModel request)
        {
            throw new NotImplementedException();
        }

        public List<FeedViewModel> GetFeeds()
        {
            List<FeedViewModel> feedViewModels = new List<FeedViewModel>();

            foreach (var item in _feeds)
            {
                feedViewModels.Add(new FeedViewModel(item.Desk, item.Foto));
            }

            return feedViewModels;
        }
    }
}