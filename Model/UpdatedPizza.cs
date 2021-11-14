using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaApi.Model
{
    public class UpdatedPizza
    {
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(3)]
        public string ShortName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EPizzaStockStatus StockStatus { get; set; }

        [MaxLength(1024)]
        public string Ingredients { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }
    }
}