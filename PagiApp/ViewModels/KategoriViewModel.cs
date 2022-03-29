using System.ComponentModel.DataAnnotations;
using PagiApp.Datas.Entities;

namespace PagiApp.ViewModels
{
    public class KategoriViewModel
    {
        public KategoriViewModel()
        {
            Nama = string.Empty;
            Deskripsi = string.Empty;
            Icon = string.Empty;
        }
        public int IdKategori { get; set; }
        [Required]
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public string Icon { get; set; }

        public KategoriProduct ConvertToDbModel(){
            return new KategoriProduct {
                IdKategori = this.IdKategori,
                Nama = this.Nama,
                Deskripsi = this.Deskripsi,
                Icon = this.Icon,
            };
        }
    }
}