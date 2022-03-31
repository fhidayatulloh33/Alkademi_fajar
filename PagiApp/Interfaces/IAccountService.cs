namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
public interface IAccountService
{
    Task<Admin> Login(string username, string password);
}