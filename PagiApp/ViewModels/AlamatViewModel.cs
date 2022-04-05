using System.ComponentModel.DataAnnotations;
using PagiApp.Datas.Entities;

namespace PagiApp.ViewModels
{
    public class AlamatViewModel
    {
        public AlamatViewModel()
        {
            Provinsi = string.Empty;
            Kabupaten = string.Empty;
            Kecamatan = string.Empty;
            Desa = string.Empty;
            RtRw = string.Empty;
            KodePos = string.Empty;
            Deskripsi = string.Empty;
        }

        public AlamatViewModel(Alamat item) {
            IdAlamat = item.IdAlamat;
            Provinsi = item.Provinsi;
            Kabupaten = item.Kabupaten;
            Kecamatan = item.Kecamatan;
            Desa = item.Desa;
            RtRw = item.RtRw;
            KodePos = item.KodePos;
            Deskripsi = item.Deskripsi;
        }

        public int IdAlamat { get; set; }
        public string Provinsi { get; set; }
        public string Kabupaten { get; set; }
        public string Kecamatan { get; set; }
        public string Desa { get; set; }
        public string RtRw { get; set; }
        public string KodePos { get; set; }
        public string Deskripsi { get; set; }

        public Alamat ConvertToDbModel(){
            return new Alamat {
                IdAlamat = this.IdAlamat,
                Provinsi = this.Provinsi,
                Kabupaten = this.Kabupaten,
                Kecamatan = this.Kecamatan,
                Desa = this.Desa,   
                RtRw = this.RtRw,
                KodePos = this.KodePos,
                Deskripsi = this.Deskripsi,
            };
        }
    }
}