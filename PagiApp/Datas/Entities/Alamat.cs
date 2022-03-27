using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Alamat
    {
        public Alamat()
        {
            Pengirimen = new HashSet<Pengiriman>();
        }

        public int IdAlamat { get; set; }
        public int IdCustomer { get; set; }
        public string Provinsi { get; set; } = null!;
        public string Kabupaten { get; set; } = null!;
        public string Kecamatan { get; set; } = null!;
        public string Desa { get; set; } = null!;
        public string RtRw { get; set; } = null!;
        public string KodePos { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
    }
}
