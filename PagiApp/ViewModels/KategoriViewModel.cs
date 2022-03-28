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
        }
        public int Id { get; set; }
        [Required]
        public string Nama { get; set; }
        public string Deskripsi { get; set; }

        public Kategori ConvertToDbModel(){
            return new Kategori {
                Id = this.Id,
                Nama = this.Nama,
                Deskripsi = this.Deskripsi,
            };
        }
    }
}