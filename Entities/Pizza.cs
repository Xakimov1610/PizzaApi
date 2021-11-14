using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApi.Entities
{
    public class Pizza
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        

        [Obsolete("Used only for entity binding.", true)]
        public Pizza() { }
        public Pizza(string title, string shortName, EPizzaStockStatus stocStatus, string ingredients, double price, EPizzaStockStatus stockStatus)
        {
            Id = Guid.NewGuid();
            Title = title;
            ShortName = shortName;
            StocStatus = stocStatus;
            Ingredients = ingredients;
            Price = price;
        }
        
        
        
        
        
        
    }
}