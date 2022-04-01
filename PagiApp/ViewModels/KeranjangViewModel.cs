using System.ComponentModel.DataAnnotations;
using PagiApp.Datas.Entities;

namespace PagiApp.ViewModels;
public class KeranjangViewModel
{
    public KeranjangViewModel()
    {
    }

    public int IdKeranjang { get; set; }
    public int IdProduct { get; set; }
    public string Image { get; set; }
    public string NamaProduk { get; set; }
    public int IdCustomer { get; set; }
    public int JmlBarang { get; set; }
    public decimal Subtotol { get; set; }

}