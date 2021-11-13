using System;
using System.Threading.Tasks;

namespace PizzaApi.Service
{
    public interface IStorageService
    {
        Task<(bool IsSuccess, Exception exception)> GetPizzaAsync();
        Task<(bool IsSuccess, Exception exception)> GetPizzaAsync(
            Guid id = default(Guid),
            string title = default(string),
            string shortName = default(string),
            Entities.EPizzaStockStatus? stockStatus = null,
            string indegrients = default(string),
            double price = default(double));
        Task<(bool IsSuccess, Exception exception)> UpdatePizzaAsync(Entities.Pizza pizza);
        Task<(bool IsSuccess, Exception exception)> InsertPizzaAsync(Entities.Pizza pizza);
        Task<(bool IsSuccess, Exception exception)> RemovePizzaAsync(Guid id);
    }
}