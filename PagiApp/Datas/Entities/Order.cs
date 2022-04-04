using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Order
    {
        public Order()
        {
            Detailorders = new HashSet<Detailorder>();
            Pembayarans = new HashSet<Pembayaran>();
            Pengirimen = new HashSet<Pengiriman>();
        }

        public int IdOrder { get; set; }
        public int IdKeranjang { get; set; }
        public DateOnly TglTransaksi { get; set; }
        public int JmlBayar { get; set; }
        public int IdCustomer { get; set; }
        public string Status { get; set; } = null!;
        public string Note { get; set; } = null!;
        public int IdStatus { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Keranjang IdKeranjangNavigation { get; set; } = null!;
        public virtual Status IdStatusNavigation { get; set; } = null!;
        public virtual ICollection<Detailorder> Detailorders { get; set; }
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
    }
}
