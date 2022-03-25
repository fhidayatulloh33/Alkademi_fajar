namespace Facebooker.Models;

public class Feed
{
//    public int Id { get; set; }
//    public string Publish { get; set; }
    public string Desk { get; set; }
    public string Foto { get; set; }
    public Feed( string desk, string foto)
    {
//       this.Id = id;
//        this.Publish = Publish;
        this.Desk = desk;
        this.Foto = foto;
    }
}