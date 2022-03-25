using Facebooker.Models;

namespace Facebooker.Services
{
    public class FileService : IFileService
    {
        private const string FILE_NAME = "_data.txt";
        public async Task<List<FeedViewModel>> Read()
        {
            if(!File.Exists(System.AppContext.BaseDirectory + FILE_NAME)){
                return new List<FeedViewModel>();
            }
            var res = await File.ReadAllLinesAsync(System.AppContext.BaseDirectory + FILE_NAME);
            if(res == null)
                return new List<FeedViewModel>();

            List<FeedViewModel> feeds = new List<FeedViewModel>();
            foreach (var item in res)
            {
                var dataSplit = item.Split(":").ToArray();
                feeds.Add(new FeedViewModel(dataSplit[0], dataSplit[1]));
            }

            return feeds;
        }

        public async Task Write(FeedViewModel request)
        {
            if(!File.Exists(System.AppContext.BaseDirectory + FILE_NAME)){
                using (var fileStream = File.Create(System.AppContext.BaseDirectory + FILE_NAME)){
                    fileStream.Close();
                }
            }
            using(var fileStream = File.AppendText(System.AppContext.BaseDirectory + FILE_NAME)){
                await fileStream.WriteLineAsync($"{request.Desk}:{request.Foto}");
            }
        }
    }
}
