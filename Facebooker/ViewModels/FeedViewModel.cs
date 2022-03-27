using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Facebooker.Models;

public class FeedViewModel
{
    public string Desk { get; set; }
    public string Foto { get; set; }
    public User User { get; set; }
    public FeedViewModel(){}
    public FeedViewModel(string desk, string foto)
    {
        Desk = desk;
        Foto = foto;
    }
    public void PostBy(User user){
        User = user;
    }
}