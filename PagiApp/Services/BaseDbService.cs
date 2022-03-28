using PagiApp.Interfaces;
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PagiApp.Services;
public class BaseDbService
{
    protected readonly pagiContext DbContext;
    public BaseDbService(pagiContext dbContext)
    {
        DbContext = dbContext;
    }
}