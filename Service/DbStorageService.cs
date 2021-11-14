using System;
using PizzaApi.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using PizzaApi.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Entities;
using PizzaApi.Service;

namespace PizzaApi.Service
{
    public class DbStoreService : IStorageService
    {
        private readonly ILogger<DbStoreService> _logger;
        private PizzaDbContext _context;

        public DbStoreService(ILogger<DbStoreService> logger, PizzaDbContext context)
        {
            _logger = logger;
            _context = context;
        }   

        public async Task<(bool IsSuccess, Exception exception, List<NewPizza> pizza)> GetPizzaAsync()
        {
            try
            {
                var pizzas = await _context.Pizzas.Select(p => new NewPizza()
                {
                    Id = p.Id,
                    Title = p.Title,
                    ShortName = p.ShortName,
                    StocStatus = (Model.EPizzaStockStatus)p.StocStatus,
                    Ingredients = p.Ingredients,
                    Price = p.Price
                }).ToListAsync();

                _logger.LogInformation("Pizzas received from datebase.");
                return (true, null, pizzas);
            }

            catch (Exception e)
            {
                _logger.LogInformation($"Receiving pizzas from database failed: {e.Message}", e);
                return (false, null, null);
            }
        }

        public async Task<(bool IsSuccess, Exception exception, Pizza pizzaResult)> GetPizzaIdAsync(Guid Id)
        {
            try
            {
                var pizzaResult = await _context.Pizzas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Id);

                if (pizzaResult is default(Pizza))
                {
                    return (false, null, null);
                }

                _logger.LogInformation($"Pizza recived from database: {Id}");
                return (true, null, pizzaResult);
            }

            catch (Exception e)
            {
                _logger.LogInformation($"Receiving pizza from database: {Id} failed");
                return (false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception exception)> InsertPizzaAsync(Pizza pizza)
        {
            try
            {
                if (!await _context.Pizzas.AnyAsync(p => p.Id == pizza.Id))
                {
                    await _context.Pizzas.AddAsync(pizza);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Pizza ordered in database: {pizza.Id}");
                    return (true, null);
                }

                else
                {
                    return (false, new Exception());
                }
            }

            catch (Exception e)
            {
                _logger.LogInformation($"Ordering pizza in database: {pizza.Id} failed");
                return (false, e);
            }
        }

        public async Task<(bool IsSuccess, Exception exception)> DeletePizzaAsync(Guid id)
        {
            try
            {
                if (await _context.Pizzas.AnyAsync(p => p.Id == id))
                {
                    _context.Pizzas.Remove(_context.Pizzas.FirstOrDefault(p => p.Id == id));
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Pizza removed from database: {id}");

                    return (true, null);
                }

                else
                {
                    return (false, null);
                }
            }

            catch (Exception e)
            {
                _logger.LogInformation($"Deleting pizza from database: {id} failed\n{e.Message}", e);
                return (false, e);
            }
        }

        public async Task<(bool IsSuccess, Exception exception, Pizza pizza)> UpdatePizzaAsync(Guid id, Pizza pizza)
        {
            try
            {
                if (await _context.Pizzas.AnyAsync(p => p.Id == id))
                {
                    _context.Pizzas.Update(pizza);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Pizza updated in database: {id}.");
                    return (true, null, pizza);
                }
                else
                {
                    return (false, new Exception(), null);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Updating with given ID: {id} not found\nError: {e.Message}");
                return (false, e, null);
            }
        }

        Task<(bool IsSuccess, Exception exception)> IStorageService.GetPizzaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception exception)> GetPizzaAsync(Guid id = default, string title = null, string shortName = null, Entities.EPizzaStockStatus? stockStatus = null, string indegrients = null, double price = 0)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception exception)> UpdatePizzaAsync(Pizza pizza)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Exception exception)> RemovePizzaAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}