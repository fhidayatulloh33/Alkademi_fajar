using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;
public class KategoriService : BaseDbService, IKategoriService
{
    public KategoriService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<KategoriProduct> Add(KategoriProduct obj)
    {
        if(await DbContext.KategoriProducts.AnyAsync(x=>x.IdKategori == obj.IdKategori)){
            throw new InvalidOperationException($"Kategori with id {obj.IdKategori} already exists");
        }
        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await DbContext.KategoriProducts.FirstOrDefaultAsync(x=>x.IdKategori == id);
        if(obj == null){
            throw new InvalidOperationException($"Kategori with id {id} not found");
        }

        DbContext.ProductKategoris.RemoveRange(DbContext.ProductKategoris.Where(x=>x.IdKategori == id));

        DbContext.Remove(obj);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<KategoriProduct>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.KategoriProducts
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<KategoriProduct> Get(int id)
    {
        var result = await DbContext.KategoriProducts.FirstOrDefaultAsync(x=>x.IdKategori == id);

        if (result == null)
        {
            throw new InvalidOperationException($"Kategori with id {id} not found");
        }

        return result;
    }

    public async Task<KategoriProduct> Get(Expression<Func<KategoriProduct, bool>> func)
    {
        throw new NotImplementedException();   
    }

    public async Task<List<KategoriProduct>> GetAll()
    {
        return await DbContext.KategoriProducts.ToListAsync();
    }

    public async Task<KategoriProduct> Update(KategoriProduct obj)
    {
        if(obj == null){
            throw new ArgumentNullException("Kategori cannot be null");
        }

        var result = await DbContext.KategoriProducts.FirstOrDefaultAsync(x=>x.IdKategori == obj.IdKategori);
        if(result == null){
            throw new InvalidOperationException($"Kategori with id {obj.IdKategori} not found");
        }

        result.Nama = obj.Nama;
        result.Deskripsi = obj.Deskripsi;
        result.Icon = obj.Icon;

        DbContext.Update(result);
        await DbContext.SaveChangesAsync();

        return result;  


    }
}
