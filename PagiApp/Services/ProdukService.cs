using System.Net.Http.Headers;
using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;
public class ProdukService : BaseDbService, IProdukService
{
    public ProdukService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<Product> Add(Product obj, int idKategori)
    {
        if(await DbContext.Products.AnyAsync(x=>x.IdProduct == obj.IdProduct)){
            throw new InvalidOperationException($"Product with ID {obj.IdProduct} is already exist");
        }

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        DbContext.ProductKategoris.Add(new ProductKategori{
            IdKategori = idKategori,
            IdProduct = obj.IdProduct,
        });

        return obj;
    }

    public async Task<Product> Add(Product obj)
    {
        if(await DbContext.Products.AnyAsync(x=>x.IdProduct == obj.IdProduct)){
            throw new InvalidOperationException($"Product with ID {obj.IdProduct} is already exist");
        }

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var Product = await DbContext.Products.FirstOrDefaultAsync(x=>x.IdProduct == id);

        if(Product == null) {
            throw new InvalidOperationException($"Product with ID {id} doesn't exist");
        }

        DbContext.ProductKategoris.RemoveRange(DbContext.ProductKategoris.Where(x=>x.IdProduct == id));
        DbContext.Remove(Product);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Product>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Products
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Product?> Get(int id)
    {
        var result = await DbContext.Products.FirstOrDefaultAsync(x=>x.IdProduct == id);

        if(result == null)
        {
            throw new InvalidOperationException($"Product with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Product?> Get(Expression<Func<Product, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetAll()
    {
        return await DbContext.Products
        .Include(x=>x.ProductKategoris)
        .ThenInclude(x=>x.IdKategoriNavigation)
        .ToListAsync();
    }

    public async Task<Product> Update(Product obj)
    {
        if(obj == null)
        {
            throw new ArgumentNullException("Product cannot be null");
        }

        var Produk = await DbContext.Products.FirstOrDefaultAsync(x=>x.IdProduct == obj.IdProduct);

        if(Produk == null) {
            throw new InvalidOperationException($"Product with ID {obj.IdProduct} doesn't exist in database");
        }

        Produk.Nama = obj.Nama;
        Produk.Deskripsi = obj.Deskripsi;
        if (!string.IsNullOrEmpty(obj.Gambar))
        {
            Produk.Gambar = obj.Gambar;
        }
        Produk.Harga = obj.Harga;

        if (obj.ProductKategoris != null && obj.ProductKategoris.Any())
        {
            Produk.ProductKategoris = obj.ProductKategoris;
        }  

        DbContext.Update(Produk);

        await DbContext.SaveChangesAsync();

        return Produk;
    }
} 