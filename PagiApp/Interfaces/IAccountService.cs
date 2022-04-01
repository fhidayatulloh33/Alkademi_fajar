namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
using PagiApp.ViewModels;

public interface IAccountService
{
    Task<Admin> Login(string username, string password);
    Task<Customer> LoginCustomer(string username, string password);
    Task<Customer> Register(RegisterViewModel request);
}