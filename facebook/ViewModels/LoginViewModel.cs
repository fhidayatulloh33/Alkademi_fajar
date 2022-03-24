using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace facebook.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public LoginViewModel()
    {

    }
}
