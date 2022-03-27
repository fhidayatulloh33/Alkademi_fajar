using System.ComponentModel.DataAnnotations;

namespace PagiApp.Models
{         
   public class Produk
   {
        public int IdProduct { get; set; }
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public decimal Harga { get; set; }
        public int Stock { get; set; }
        public string Gambar { get; set; }
   }
}
