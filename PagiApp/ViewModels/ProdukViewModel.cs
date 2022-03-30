using System.ComponentModel.DataAnnotations;
using PagiApp.Datas.Entities;

namespace PagiApp.ViewModels;
public class ProdukViewModel
{
    public ProdukViewModel()
    {
        Kategories = new List<KategoriViewModel>();   
    }
    public ProdukViewModel(int idProduct, string nama, string deskripsi, decimal harga)
    {
        IdProduct = idProduct;
        Nama = nama;
        Deskripsi = deskripsi;
        Harga = harga;
        Stock = 100;
        KategoriId = Array.Empty<int>();
        Kategories = new List<KategoriViewModel>();
    }

    public ProdukViewModel(Product item)
    {
        IdProduct = item.IdProduct;
        Nama = item.Nama;
        Deskripsi = item.Deskripsi;
        Harga = item.Harga;
        Stock = item.Stock;
        KategoriId = item.ProductKategoris.Select(x => x.IdKategori).ToArray();
        Gambar = item.Gambar;
    }

    public int IdProduct { get; set; }
    [Required]
    public string Nama { get; set; } = null!;
    public string Deskripsi { get; set; } = null!;
    [Required]
    public decimal Harga { get; set; }
    public int Stock { get; set; }
    public string? Gambar { get; set; }
    public string GambarSrc{
        get{
            return (string.IsNullOrEmpty(Gambar) ? "images/default.PNG" : Gambar);
        }
    }
    public IFormFile? GambarFile { get; set; }
    public int[] KategoriId { get; set; }
    public List<KategoriViewModel> Kategories { get; set; }

    public Product ConvertToDbModel(){
        return new Product() {
            IdProduct = this.IdProduct,
            Nama = this.Nama,
            Deskripsi = this.Deskripsi,
            Harga = this.Harga,
            Gambar = this.Gambar ?? string.Empty,
            Stock = this.Stock
        };
    }
}