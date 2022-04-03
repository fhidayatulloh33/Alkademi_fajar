using System.ComponentModel.DataAnnotations;
using PagiApp.Datas.Entities;

namespace PagiApp.ViewModels;
public class ProdukDetailViewModel: ProdukViewModel
{
    public ProdukDetailViewModel()
    {
        Kategories = new List<KategoriViewModel>();
    }

    public ProdukDetailViewModel(int id, string nama, string deskripsi, decimal harga)
    {
        IdProduct = id;
        Nama = nama;
        Harga = harga;
        Kategories = new List<KategoriViewModel>();
    }
    public string Deskripsi { get; set; }
    public int Stok { get; set; }
    public int Terjual { get; set; }

}
