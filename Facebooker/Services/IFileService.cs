using Facebooker.Models;

namespace Facebooker.Services
{
    public interface IFileService
    {
        Task<List<FeedViewModel>> Read();
        Task Write(FeedViewModel request);
    }
}
