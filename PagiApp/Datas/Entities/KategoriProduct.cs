using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class KategoriProduct
    {
        public KategoriProduct()
        {
            ProductKategoris = new HashSet<ProductKategori>();
        }

        public int IdKategori { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
        public string Icon { get; set; } = null!;

        public virtual ICollection<ProductKategori> ProductKategoris { get; set; }
    }
}
