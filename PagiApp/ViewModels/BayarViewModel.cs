using System.ComponentModel.DataAnnotations;

namespace PagiApp.ViewModels;
public class BayarViewModel {
    public BayarViewModel()
    {

    }
    [Required]
    public int IdOrder { get; set; }
    public int IdCustomer { get; set; }
    //[Required]
    public DateOnly TglBayar { get; set; }
    //[Required]
    public int JmlBayar { get; set; }
    [Required]
    public string MetodeBayar { get; set; }
    [Required]
    public string IdTujuan { get; set; }
    [Required]
    public IFormFile FileBuktiBayar { get; set; }
}