using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Keranjang
    {
        public int IdKeranjang { get; set; }
        public int IdCustomer { get; set; }
        public int IdProduct { get; set; }
        public int JmlBarang { get; set; }
        public int Subtotol { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
