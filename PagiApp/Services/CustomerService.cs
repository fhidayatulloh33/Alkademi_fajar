using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;
public class CustomerService : BaseDbService, ICustomerService
{
    public CustomerService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<Customer> Add(Customer obj)
    {
        if(await DbContext.Customers.AnyAsync(x=>x.IdCustomer == obj.IdCustomer)){
            throw new InvalidOperationException($"Customer with id {obj.IdCustomer} already exists");
        }
        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await DbContext.Customers.FirstOrDefaultAsync(x=>x.IdCustomer == id);
        if(obj == null){
            throw new InvalidOperationException($"Kategori with id {id} not found");
        }

        DbContext.Customers.RemoveRange(DbContext.Customers.Where(x=>x.IdCustomer == id));

        DbContext.Remove(obj);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Customer>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Customers
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Customer> Get(int id)
    {
        var result = await DbContext.Customers.FirstOrDefaultAsync();

        if (result == null)
        {
            throw new InvalidOperationException($"Customer with id {id} not found");
        }

        return result;
    }

    public async Task<Customer> Get(Expression<Func<Customer, bool>> func)
    {
        throw new NotImplementedException();   
    }

    public async Task<List<Customer>> GetAll()
    {
        return await DbContext.Customers.ToListAsync();
    }

    public async Task<Customer> Update(Customer obj)
    {
        if(obj == null){
            throw new ArgumentNullException("customer cannot be null");
        }

        var result = await DbContext.Customers.FirstOrDefaultAsync(x=>x.IdCustomer == obj.IdCustomer);
        if(result == null){
            throw new InvalidOperationException($"Customer with id {obj.IdCustomer} not found");
        }

        DbContext.Update(result);
        await DbContext.SaveChangesAsync();

        return result;  


    }
}