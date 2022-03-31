namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
public interface IProdukKategoriService
{
    Task<int[]> GetKategoriIds(int produkId);
    Task Remove(int produkId, int idKategori);
}