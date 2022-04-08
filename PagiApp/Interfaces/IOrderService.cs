namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
using PagiApp.ViewModels;

public interface IOrderService
{
    Task<Order> Checkout(Order newOrder);
    Task<List<OrderViewModel>> Get(int idCustomer);  
    Task<List<OrderViewModel>> Getv2(int limit, int offset, int? status, DateTime? date);
    Task<OrderViewModel> GetDetail(int idOrder, int idCustomer);
    Task<OrderViewModel> GetDetail(int idOrder);
    Task UpdateStatus(int idOrder, short dIBAYAR);
    Task Bayar(Pembayaran newBayar);
    Task<bool> SudahDibayar(int idOrder);
    Task Kirim(Pengiriman dataPengiriman);
    Task Ulas(Ulasan ulasan);
}