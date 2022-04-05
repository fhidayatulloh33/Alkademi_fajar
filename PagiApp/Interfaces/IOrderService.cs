namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
using PagiApp.ViewModels;

public interface IOrderService
{
    Task<Order> Checkout(Order newOrder);  
    Task<List<OrderViewModel>> Get(int idCustomer);   
}