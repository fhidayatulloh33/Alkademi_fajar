using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Pembayaran
    {
        public int IdPembayaran { get; set; }
        public string MetodeBayar { get; set; } = null!;
        public int JumlahBayar { get; set; }
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public DateOnly TglBayar { get; set; }
        public int Pajak { get; set; }
        public string Tujuan { get; set; } = null!;

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
