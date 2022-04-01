using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PagiApp.ViewModels;
using System.Linq;

namespace PagiApp.Services;
public class KeranjangService : BaseDbService, IKeranjangService
{
    private readonly IProdukService _produkService;
    public KeranjangService(pagiContext dbContext, IProdukService produkService
    ) : base(dbContext)
    {
        _produkService = produkService;
    }

    public async Task<Keranjang> Add(Keranjang obj)
    {
        if(await DbContext.Keranjangs.AnyAsync(x=>x.IdProduct == obj.IdProduct && x.IdCustomer == obj.IdCustomer))
        {
            return obj;
        }

        //get data produk
        var produk = await _produkService.Get(obj.IdProduct);

        if(produk == null)
        {
            throw new InvalidOperationException("Produk tidak ditemukan");
        }

        if(obj.JmlBarang < 1) 
        {
            obj.JmlBarang = 1;
        }

        int HargaConvert = Convert.ToInt32(produk.Harga);
        //rumus subtotal = harga * jumlah produk
        obj.Subtotol = HargaConvert * obj.JmlBarang;

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Keranjang>> Get(int limit, int offset, string keyword)
    {
        throw new NotImplementedException();
    }

    public Task<Keranjang?> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Keranjang?> Get(Expression<Func<Keranjang, bool>> func)
    {
        throw new NotImplementedException();
    }

    public Task<List<Keranjang>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Keranjang> Update(Keranjang obj)
    {
        throw new NotImplementedException();
    }

    async Task<List<KeranjangViewModel>> IKeranjangService.Get(int idCustomer)
    {
        var result = await (from a in DbContext.Keranjangs
        join b in DbContext.Products on a.IdProduct equals b.IdProduct
        where a.IdCustomer == idCustomer
        select new KeranjangViewModel 
        {
            IdKeranjang = a.IdKeranjang,
            IdCustomer = a.IdCustomer,
            IdProduct = a.IdProduct,
            Image = b.Gambar,
            JmlBarang  = a.JmlBarang,
            Subtotol  = a.Subtotol,
            NamaProduk = b.Nama
        }).ToListAsync();

        return result;
    }
}