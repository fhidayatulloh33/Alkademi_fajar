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

    public async Task<ActionResult> Add(KategoriProducts obj)
    {
        if(await DbContext.KategoriProducts.AnyAsync(obj)){
            throw new InvalidOperationException($"Kategori with id {obj.Id} already exists");
        }
        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<ActionResult> Delete(int id)
    {
        var obj = await DbContext.KategoriProducts.FirstOrDefaultAsync(x=>x.Id == id);
        if(obj == null){
            throw new InvalidOperationException($"Kategori with id {id} not found");
        }

        DbContext.Remove(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<List<KategoriProducts>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.KategoriProducts
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<KategoriProducts> Get(int id)
    {
        var result = await DbContext.KategoriProducts.FirstOrDefaultAsync();

        if (result == null)
        {
            throw new InvalidOperationException($"Kategori with id {id} not found");
        }

        return result;
    }

    public async Task<KategoriProducts> Get(Expression<Func<KategoriProducts, bool>> func)
    {
        throw new NotImplementedException();   
    }

    public async Task<List<KategoriProducts>> GetAll()
    {
        return await DbContext.KategoriProducts.ToListAsync();
    }

    public async Task<ActionResult> Update(KategoriProducts obj)
    {
        if(obj == null){
            throw new ArgumentNullException("Kategori cannot be null");
        }

        var result = await DbContext.KategoriProducts.FirstOrDefaultAsync(x=>x.Id == obj.Id);
        if(result == null){
            throw new InvalidOperationException($"Kategori with id {obj.Id} not found");
        }

        KategoriProduct.Name = obj.Name;
        KategoriProduct.Description = obj.Description;

        await DbContext.UpdateAsync(result);
        await DbContext.SaveChangesAsync();

        return kategori;  


    }
}
