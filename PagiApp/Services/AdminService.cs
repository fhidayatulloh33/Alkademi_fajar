using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;
public class AdminService : BaseDbService, IAdminService
{
    public AdminService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<Admin> Add(Admin obj)
    {
        if(await DbContext.Admins.AnyAsync(x=>x.IdAdmin == obj.IdAdmin)){
            throw new InvalidOperationException($"Kategori with id {obj.IdAdmin} already exists");
        }
        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await DbContext.Admins.FirstOrDefaultAsync(x=>x.IdAdmin == id);
        if(obj == null){
            throw new InvalidOperationException($"Kategori with id {id} not found");
        }

        DbContext.Admins.RemoveRange(DbContext.Admins.Where(x=>x.IdAdmin == id));

        DbContext.Remove(obj);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Admin>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Admins
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Admin> Get(int id)
    {
        var result = await DbContext.Admins.FirstOrDefaultAsync();

        if (result == null)
        {
            throw new InvalidOperationException($"Admin with id {id} not found");
        }

        return result;
    }

    public async Task<Admin> Get(Expression<Func<Admin, bool>> func)
    {
        throw new NotImplementedException();   
    }

    public async Task<List<Admin>> GetAll()
    {
        return await DbContext.Admins.ToListAsync();
    }

    public async Task<Admin> Update(Admin obj)
    {
        if(obj == null){
            throw new ArgumentNullException("Kategori cannot be null");
        }

        var result = await DbContext.Admins.FirstOrDefaultAsync(x=>x.IdAdmin == obj.IdAdmin);
        if(result == null){
            throw new InvalidOperationException($"Kategori with id {obj.IdAdmin} not found");
        }

        DbContext.Update(result);
        await DbContext.SaveChangesAsync();

        return result;  


    }
}