namespace facebook.Models;

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string fotoProfilLink { get; set; }

    public User(int id, string username, string password, string fotoProfilLink)
    {
        this.id = id;
        this.username = username;
        this.password = password;
        this.fotoProfilLink = fotoProfilLink;
    }
   
}