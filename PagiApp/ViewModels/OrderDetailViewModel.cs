namespace PagiApp.ViewModels;
public class OrderDetailViewModel {
    public int IdOrder { get; set; }
    public string Produk { get; set; }
    public decimal Harga {get;set;}
    public int Qty {get;set;}
    public decimal SubTotal {get;set;}
}