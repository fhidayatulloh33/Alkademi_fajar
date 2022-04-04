using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Detailorder
    {
        public int IdDetaiOrder { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public decimal Harga { get; set; }
        public int JmlBarang { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
