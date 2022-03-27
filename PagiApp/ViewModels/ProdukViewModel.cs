namespace PagiApp.ViewModels
{
    public class ProdukViewModel
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Desk { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public string Gambar { get; set; }
        public ProdukViewModel(){}
    }
}