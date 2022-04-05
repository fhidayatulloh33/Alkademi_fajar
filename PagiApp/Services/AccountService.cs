using PagiApp.Interfaces;
using PagiApp.ViewModels;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;

public class AccountService : BaseDbService, IAccountService
{
    public AccountService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<Admin> Login(string username, string password)
    {
        var result = await DbContext.Admins.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

        return result;
    }

    public async Task<Customer> LoginCustomer(string username, string password)
    {
        return await DbContext.Customers.FirstOrDefaultAsync(x=>x.Username == username && x.Password == password);
    }

    public async Task<Customer> Register(RegisterViewModel request){
        //check username
        if(await DbContext.Customers.AnyAsync(x=>x.Username == request.Username)){
            throw new InvalidOperationException($"{request.Username} already exist");
        }
        
        //check nohp
        if(await DbContext.Customers.AnyAsync(x=>x.NoHp == request.NoHp)){
            throw new InvalidOperationException($"{request.NoHp} already exist");
        }

        var newCustomer = request.ConvertToDataModel();
        await DbContext.Customers.AddAsync(newCustomer);

        await DbContext.SaveChangesAsync();

        return newCustomer; 
    }
    
    public async Task<List<Tuple<int, string>>> GetAlamat(int idCustomer){
        return await DbContext.Alamats.Where(x=>x.IdCustomer == idCustomer)
        .Select(x => new Tuple<int, string>(x.IdAlamat, x.Deskripsi))
        .ToListAsync();
    }
}