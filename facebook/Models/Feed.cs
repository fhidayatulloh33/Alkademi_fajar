namespace facebook.Models;

public class Feed
{
    public int id { get; set; }
    public string usernameFeed { get; set; }
    public string fotoProfilFeed { get; set; }
    public string tanggalFeed { get; set; }
    public string postFeed { get; set; }
    public string fotoFeedLink { get; set; }
    public User user { get; set; }

    public Feed (int id, string fotoProfilFeed, string usernameFeed, string tanggalFeed, string postFeed, string fotoFeedLink, User user)
    {
        this.id = id;
        this.usernameFeed = usernameFeed;
        this.fotoProfilFeed = fotoProfilFeed;
        this.tanggalFeed = tanggalFeed;
        this.postFeed = postFeed;
        this.fotoFeedLink = fotoFeedLink;
        this.user = user;
    }
}