using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaApi.Model
{
    public class NewPizza
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(3)]
        public string ShortName { get; set; }
        
        public EPizzaStockStatus StocStatus { get; set; }  

        [Required]
        [MaxLength(1024)]
        public string Ingredients { get; set; }
        
        [Required]
        [MinLength(0)]
        [MaxLength(1000)]
        public double Price { get; set; }
    }
}