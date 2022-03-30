namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
public interface IProdukService : ICrudService<Product>
{
    Task<Product> Add(Product obj, int idKategori);
}