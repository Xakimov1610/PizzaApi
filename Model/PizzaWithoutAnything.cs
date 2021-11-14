using System;

namespace PizzaApi.Model
{
    public class PizzaWithoutAnything
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ShortName { get; set; }

        public EPizzaStockStatus StockStatus { get; set; }

        public string Ingridients { get; set; }

        public double Price { get; set; }
    }
}