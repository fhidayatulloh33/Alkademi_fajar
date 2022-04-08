using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
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
        join b in DbContext.StatusOrders on a.Status equals b.IdStatus
        join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
        where a.IdCustomer == idCustomer
        select new OrderViewModel
        {
            IdOrder = a.IdOrder,
            Status = b.Nama,
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

    public async Task<List<OrderViewModel>> Getv2(int limit, int offset, int? status, DateTime? date)
    {
        var selectCondition = (from a in DbContext.Orders
                                join b in DbContext.StatusOrders on a.Status equals b.IdStatus
                                join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                                select new OrderViewModel
                                {
                                    IdOrder = a.IdOrder,
                                    Status = b.Nama,
                                    TglOrder = a.TglTransaksi,
                                    TotalBayar = a.JmlBayar
                                }).AsQueryable();

        if(status != null)
        {
            selectCondition = selectCondition.Where(x=>x.IdStatus == status.Value);
        }

        if(date != null)
        {
            selectCondition = selectCondition.Where(x=>x.TglOrder.Date == date.Value.Date);
        }

        return await selectCondition
        .Skip(offset)
        .Take(limit)
        .ToListAsync();
    }

    public async Task<OrderViewModel> GetDetail(int idOrder, int idCustomer)
    {
        var result = await (from a in DbContext.Orders
        join b in DbContext.StatusOrders on a.Status equals b.IdStatus
        join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
        //Left Join
        join pembayaran in DbContext.Pembayarans on a.IdOrder equals pembayaran.IdOrder
        into tempPembayaran from pembayaran in tempPembayaran.DefaultIfEmpty()
        //end join

        join pengiriman in DbContext.Pengirimen on a.IdOrder equals pengiriman.IdOrder into tempPengiriman
        from pengiriman in tempPengiriman.DefaultIfEmpty()

        where a.IdCustomer == idCustomer && a.IdOrder == idOrder
        select new OrderViewModel
        {
            IdOrder = a.IdOrder,
            IdAlamat = a.IdAlamat,
            Status = b.Nama,
            IdStatus = b.IdStatus,
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
                }).ToList(),
            Pembayaran = pembayaran == null ? new PembayaranViewModel() : new PembayaranViewModel{
                IdPembayaran = pembayaran.IdPembayaran,
                TglBayar = pembayaran.TglBayar,
                //Status = pembayaran.Status,
                FileBuktiPembayaran = pembayaran.FileBuktiPembayaran,
                Pajak = pembayaran.Pajak,
                Tujuan = pembayaran.Tujuan
            },
            Pengiriman = pengiriman == null? new PengirimanViewModel() : new PengirimanViewModel
                {
                    IdPengiriman = pengiriman.IdPengiriman,
                    Keterangan = pengiriman.Keterangan,
                    Kurir = pengiriman.Kurir,
                    NoResi = pengiriman.NoResi,
                    //Ongkir = pengiriman.Ongkir,
                    Status = pengiriman.Status
                }
        }).FirstOrDefaultAsync();

        return result;
    }

    public async Task<OrderViewModel> GetDetail(int idOrder)
    {
        var result = await (from a in DbContext.Orders
        join b in DbContext.StatusOrders on a.Status equals b.IdStatus
        join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
        //Left Join
        join pembayaran in DbContext.Pembayarans on a.IdOrder equals pembayaran.IdOrder
        into tempPembayaran from pembayaran in tempPembayaran.DefaultIfEmpty()
        //end join
        where a.IdOrder == idOrder
        select new OrderViewModel
        {
            IdOrder = a.IdOrder,
            IdAlamat = a.IdAlamat,
            Status = b.Nama,
            IdStatus = b.IdStatus,
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
                }).ToList(),
            Pembayaran = pembayaran == null ? new PembayaranViewModel() : new PembayaranViewModel{
                IdPembayaran = pembayaran.IdPembayaran,
                FileBuktiPembayaran = pembayaran.FileBuktiPembayaran,
                Pajak = pembayaran.Pajak,
                JumlahBayar = pembayaran.JumlahBayar,
                Tujuan = pembayaran.Tujuan
            }
        }).FirstOrDefaultAsync();

        return result;
    }

    public async Task UpdateStatus(int idOrder, short dIBAYAR)
    {
        var order = await DbContext.Orders.FirstOrDefaultAsync(x => x.IdOrder == idOrder);

        if (order == null)
        {
            throw new ArgumentNullException("Data order tidak ditemukan");
        }

        order.Status = dIBAYAR;

        DbContext.Update(order);
        await DbContext.SaveChangesAsync();
    }

    public async Task Bayar(Pembayaran newBayar)
    {
        if (await DbContext.Pembayarans.AnyAsync(x => x.IdOrder == newBayar.IdOrder))
        {
            throw new InvalidOperationException("Pembayaran sudah dilakukan");
        }

        await DbContext.AddAsync(newBayar);
        await DbContext.SaveChangesAsync();

    }

    public async Task<bool> SudahDibayar(int idOrder)
    {
        return await DbContext.Orders.AnyAsync(x=>x.IdOrder == idOrder && x.Status == AppConstant.StatusOrder.DIBAYAR);
    }

    public async Task Kirim(Pengiriman dataPengiriman)
    {
        if(await DbContext.Pengirimen.AnyAsync(x=>x.IdOrder == dataPengiriman.IdOrder))
        {
            throw new InvalidOperationException("Pengiriman sudah dilakukan");
        }

        await DbContext.AddAsync(dataPengiriman);
        await DbContext.SaveChangesAsync();
    }

    public async Task Ulas(Ulasan ulasan)
    {
        if(await DbContext.Ulasans.AnyAsync(x=>x.IdOrder == ulasan.IdOrder))
        {
            throw new InvalidOperationException("Anda sudah memberikan review");
        }

        await DbContext.AddAsync(ulasan);
        await DbContext.SaveChangesAsync();
    }

}