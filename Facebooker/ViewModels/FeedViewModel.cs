using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Facebooker.Models;

public class FeedViewModel
{
    public string Desk { get; set; }
    public string Foto { get; set; }
    public User User { get; set; }
    public FeedViewModel(string desk, string foto)
    {
        this.Desk = desk;
        this.Foto = foto;
//        this.User = user;
    }
    public void PostBy(User user){
        User = user;
    }
}