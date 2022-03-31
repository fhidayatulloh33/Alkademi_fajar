using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;
public class ProdukKatgoriService : BaseDbService, IProdukKategoriService
{
    public ProdukKatgoriService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<int[]> GetKategoriIds(int produkId)
    {
        var result = await DbContext.ProductKategoris
        .Where(x=>x.IdProduct == produkId)
        .Select(x=>x.IdKategori)
        .Distinct()
        .ToArrayAsync();

        return result;
    }

    public async Task Remove(int produkId, int idKategori)
    {
        var item = await DbContext.ProductKategoris.FirstOrDefaultAsync(x => x.IdProduct == produkId && x.IdKategori == idKategori);

        if (item == null)
        {
            return;
        }

        DbContext.ProductKategoris.Remove(item);

        await DbContext.SaveChangesAsync();
    }
}