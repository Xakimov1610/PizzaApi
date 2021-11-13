namespace PizzaApi.Mapper
{
   public static class ModelEntityMapper
    {
        public static PizzaApi.Entities.Pizza ToTaskEntity(this Model.NewPizza newPizza)
        {
            return new PizzaApi.Entities.Pizza(
                title: newPizza.Title,
                shortName: newPizza.ShortName is null ? string.Empty : string.Join(',', newPizza.ShortName),
                stocStatus: newPizza.StocStatus.ToEntityPizzaStockStatus(),
                ingredients: newPizza.Ingredients,
                price: newPizza.Price
            );
        }

        public static PizzaApi.Entities.Pizza ToTaskEntity(this Model.UpdatedTask task)
        {
            return new PizzaApi.Entities.Pizza(
                title: task.Title,
                description: task.Description,
                tags: task.Tags is null ? string.Empty : string.Join(',', task.Tags),
                location: task.Location is null ? string.Empty : string.Format($"{task.Location.Latitude},{task.Location.Longitude}"),
                atATime: task.AtATime,
                onADay: task.OnADay,
                repeat: task.Repeat.ToEntityETaskRepeat(),
                status: task.Status.ToEntityETaskStatus(),
                priority: task.Priority.ToEntityETaskPriority(),
                url: task.Url)
                {
                    Id = task.Id
                };
        }
    }
}
 
