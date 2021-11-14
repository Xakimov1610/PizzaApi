using PizzaApi.Entities;
using PizzaApi.Model;

namespace PizzaApi.Mapper
{
   public static class ModelEntityPizzaMapper
    {
        public static Pizza ToPizzaEntities(this NewPizza newPizza)
        {
            return new Pizza(
                title: newPizza.Title,
                shortName: newPizza.ShortName,
                stockStatus: newPizza.StockStatus.ToEntitiesStockStatus(),
                ingredients: newPizza.Ingredients,
                price: newPizza.Price
            );
        }
        public static Pizza ToPizzaEntities(this UpdatedPizza updatedPizza)
        {
            return new Pizza(
                title: updatedPizza.Title,
                shortName: updatedPizza.ShortName,
                stockStatus: updatedPizza.StockStatus.ToEntitiesStockStatus(),
                ingredients: updatedPizza.Ingredients,
                price: updatedPizza.Price
            );
        }
        public static Entities.EPizzaStockStatus ToEntitiesStockStatus(this Model.EPizzaStockStatus stockStatus)
        {
            return stockStatus switch
            {
                Model.EPizzaStockStatus.In => Entities.EPizzaStockStatus.In,
                _ => Entities.EPizzaStockStatus.Out
            };
        }
    }
}
 
