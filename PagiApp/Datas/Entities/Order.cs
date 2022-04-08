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
            Ulasans = new HashSet<Ulasan>();
        }

        public int IdOrder { get; set; }
        public DateTime TglTransaksi { get; set; }
        public int JmlBayar { get; set; }
        public int IdCustomer { get; set; }
        public int Status { get; set; }
        public string Note { get; set; } = null!;
        public int IdAlamat { get; set; }

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual StatusOrder StatusNavigation { get; set; } = null!;
        public virtual ICollection<Detailorder> Detailorders { get; set; }
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
        public virtual ICollection<Ulasan> Ulasans { get; set; }
    }
}
