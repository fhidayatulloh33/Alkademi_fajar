using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Product
    {
        public Product()
        {
            Detailorders = new HashSet<Detailorder>();
            Keranjangs = new HashSet<Keranjang>();
            ProductKategoris = new HashSet<ProductKategori>();
        }

        public int IdProduct { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
        public decimal Harga { get; set; }
        public int Stock { get; set; }
        public string Gambar { get; set; } = null!;

        public virtual ICollection<Detailorder> Detailorders { get; set; }
        public virtual ICollection<Keranjang> Keranjangs { get; set; }
        public virtual ICollection<ProductKategori> ProductKategoris { get; set; }
    }
}
