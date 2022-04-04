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
}