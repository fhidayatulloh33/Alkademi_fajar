using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Pengiriman
    {
        public int IdPengiriman { get; set; }
        public int IdOrder { get; set; }
        public int IdAlamat { get; set; }
        public string Kurir { get; set; } = null!;
        public int Ongkir { get; set; }

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
