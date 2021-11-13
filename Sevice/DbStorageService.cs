using System;
using System.Threading.Tasks;
using PizzaApi.Entities;

namespace PizzaApi.Service
{
    public class DbSorageService : IStorageService
    {
        public Task<(bool IsSuccess, Exception exception)> GetPizzaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception exception)> GetPizzaAsync(Guid id = default, string title = null, string shortName = null, EPizzaStockStatus? stockStatus = null, string indegrients = null, double price = 0)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception exception)> InsertPizzaAsync(Pizza pizza)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception exception)> RemovePizzaAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception exception)> UpdatePizzaAsync(Pizza pizza)
        {
            throw new NotImplementedException();
        }
    }
}