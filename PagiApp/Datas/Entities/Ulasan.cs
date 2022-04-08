using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Ulasan
    {
        public int IdUlasan { get; set; }
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public string Komentar { get; set; } = null!;
        public string Gambar { get; set; } = null!;
        public int Rating { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
