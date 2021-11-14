using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaApi.Entities;
using PizzaApi.Model;

namespace PizzaApi.Service
{
   public interface IStorageService
    {
        Task<(bool IsSuccess, Exception exception)> InsertPizzaAsync(Pizza pizza);
        Task<(bool IsSuccess, Exception exception, List<Model.PizzaWithoutAnything> pizza)> GetPizzaAsync();
        Task<(bool IsSuccess, Exception exception, Pizza pizzaResult)> GetPizzaAsync(Guid Id);
        Task<(bool IsSuccess, Exception exception, UpdatedPizza updatedPizza)> UpdatePizzaAsync(Guid id, UpdatedPizza pizza);
    }
}