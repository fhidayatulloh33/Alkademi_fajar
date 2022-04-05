using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PagiApp.ViewModels;

namespace PagiApp.Services;
public class OrderService : BaseDbService, IOrderService
{
    public OrderService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<Order> Checkout(Order newOrder)
    {
        await DbContext.AddAsync(newOrder);
        await DbContext.SaveChangesAsync();

        return newOrder;
    }
    public async Task<List<OrderViewModel>> Get(int idCustomer)
    {
        var result = await (from a in DbContext.Orders
        // join b in DbContext.Status on a.Status equals b.IdOrder
        join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
        select new OrderViewModel
        {
            IdOrder = a.IdOrder,
            //Status = b.Nama,
            TglOrder = a.TglTransaksi,
            TotalBayar = a.JmlBayar,
            Details = (from c in DbContext.Detailorders
                join d in DbContext.Products on c.IdProduct equals d.IdProduct
                where c.IdOrder == a.IdOrder
                select new OrderDetailViewModel
                {
                    IdOrder = c.IdOrder,
                    Produk = d.Nama,
                    Harga = c.Harga,
                    Qty = c.JmlBarang,
                    SubTotal = c.SubTotal
                }).ToList()
        }).ToListAsync();

        return result;
    }
}