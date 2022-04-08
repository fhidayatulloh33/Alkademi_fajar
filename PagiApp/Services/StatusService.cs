using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PagiApp.ViewModels;

namespace PagiApp.Services;
public class StatusService : BaseDbService, IStatusService
{
    public StatusService(pagiContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<StatusOrder>> Get()
    {
        return await DbContext.StatusOrders.ToListAsync();
    }
}