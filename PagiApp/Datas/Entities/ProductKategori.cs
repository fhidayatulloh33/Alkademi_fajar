using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class ProductKategori
    {
        public int IdProductKategori { get; set; }
        public int IdProduct { get; set; }
        public int IdKategori { get; set; }

        public virtual KategoriProduct IdKategoriNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
