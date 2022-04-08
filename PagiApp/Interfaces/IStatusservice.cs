namespace PagiApp.Interfaces;
using PagiApp.Datas.Entities;
using PagiApp.ViewModels;

public interface IStatusService
{
    Task<List<StatusOrder>> Get();
}