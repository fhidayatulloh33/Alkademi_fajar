using System;
using System.Collections.Generic;

namespace PagiApp.Datas.Entities
{
    public partial class Status
    {
        public int IdStatus { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
    }
}
