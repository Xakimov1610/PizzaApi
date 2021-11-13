using Microsoft.EntityFrameworkCore;
using PizzaApi.Entities;

namespace PizzaApi.Data
{
   public class PizzaDbContext : DbContext
{
    public DbSet<Pizza> Pizzas { get; set; }

    public PizzaDbContext(DbContextOptions options)
        : base(options) { }
}
}