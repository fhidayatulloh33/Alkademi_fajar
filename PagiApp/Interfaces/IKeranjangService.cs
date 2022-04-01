namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
using PagiApp.ViewModels;

public interface IKeranjangService : ICrudService<Keranjang>
{
    Task<List<KeranjangViewModel>> Get(int idCustomer);   
}