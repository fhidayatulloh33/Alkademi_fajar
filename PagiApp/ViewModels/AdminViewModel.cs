using System.ComponentModel.DataAnnotations;
using PagiApp.Datas.Entities;

namespace PagiApp.ViewModels
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {  
            Nama = string.Empty;
            NoHp = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
        }

        public AdminViewModel(Admin item) {
            IdAdmin = item.IdAdmin;
            Nama = item.Nama;
            NoHp = item.NoHp;
            Username = item.Username;
            Password = item.Password;
        }

        public int IdAdmin { get; set; }
        [Required]
        public string Nama { get; set; }
        public string NoHp { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Admin ConvertToDbModel(){
            return new Admin {
                IdAdmin = this.IdAdmin,
                Nama = this.Nama,
                NoHp = this.NoHp,
                Username = this.Username,
                Password = this.Password,
            };
        }
    }
}