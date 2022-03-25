namespace Facebooker.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public User(int id, string username, string Password)
    {
        this.Id = id;
        this.Username = username;
        this.Password = Password;
    }
}