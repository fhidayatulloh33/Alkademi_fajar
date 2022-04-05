using System.Reflection;
using System.Runtime.Intrinsics.X86;
using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;
public class AlamatService : BaseDbService, IAlamatService
{
    public AlamatService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<Alamat> Add(Alamat obj)
    {
        if(await DbContext.Alamats.AnyAsync(x=>x.IdAlamat == obj.IdAlamat)){
            throw new InvalidOperationException($"Alamat with id {obj.IdAlamat} already exists");
        }
        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await DbContext.Alamats.FirstOrDefaultAsync(x=>x.IdAlamat == id);
        if(obj == null){
            throw new InvalidOperationException($"Alamat with id {id} not found");
        }

        DbContext.Alamats.RemoveRange(DbContext.Alamats.Where(x=>x.IdAlamat == id));

        DbContext.Remove(obj);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Alamat>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Alamats
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Alamat> Get(int id)
    {
        var result = await DbContext.Alamats.FirstOrDefaultAsync(x=>x.IdAlamat == id);

        if (result == null)
        {
            throw new InvalidOperationException($"Alamat with id {id} not found");
        }

        return result;
    }

    public async Task<Alamat> Get(Expression<Func<Alamat, bool>> func)
    {
        throw new NotImplementedException();   
    }

    public async Task<List<Alamat>> GetAll()
    {
        return await DbContext.Alamats.ToListAsync();
    }

    public async Task<Alamat> Update(Alamat obj)
    {
        if(obj == null){
            throw new ArgumentNullException("Kategori cannot be null");
        }

        var result = await DbContext.Alamats.FirstOrDefaultAsync(x=>x.IdAlamat == obj.IdAlamat);
        if(result == null){
            throw new InvalidOperationException($"Alamat with id {obj.IdAlamat} not found");
        }

        result.Provinsi = obj.Provinsi;
        result.Kabupaten = obj.Kabupaten;
        result.Kecamatan = obj.Kecamatan;
        result.Desa = obj.Desa;
        result.RtRw = obj.RtRw;
        result.KodePos = obj.KodePos;
        result.Deskripsi = obj.Deskripsi;

        DbContext.Update(result);
        await DbContext.SaveChangesAsync();

        return result;  

    }
}
