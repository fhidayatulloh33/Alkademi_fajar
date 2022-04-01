using System.ComponentModel.DataAnnotations;
using PagiApp.Datas.Entities;

namespace PagiApp.ViewModels;

public class RegisterViewModel
{
    public RegisterViewModel()
    {
        Username = string.Empty;
        Password = string.Empty;
    }
    public RegisterViewModel(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    [Required]
    public string Nama { get; set; } = null!;
    [Required]
    public string NoHp { get; set; } = null!;
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    public Customer ConvertToDataModel() {
        return new Customer {
            Nama = this.Nama,
            NoHp = this.NoHp,
            Username = this.Username,
            Password = this.Password,
            Foto = string.Empty
        };
    }
}