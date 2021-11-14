using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaApi.Mapper;
using PizzaApi.Model;
using PizzaApi.Service;

namespace PizzaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IStorageService _pizzaStore;

        public PizzaController(ILogger<PizzaController> logger, IStorageService pizzaStore)
        {
            _logger = logger;
            _pizzaStore = pizzaStore;
        }

        [HttpPost]
        [ActionName(nameof(CreatePizza))]
        public async Task<IActionResult> CreatePizza([FromBody] NewPizza newPizza)
        {
            var pizzaEntity = newPizza.ToPizzaEntities();
            var pizzaResult = await _pizzaStore.InsertPizzaAsync(pizzaEntity);

            if (pizzaResult.IsSuccess)
            {
                return CreatedAtAction(nameof(CreatePizza), new { id = pizzaEntity.Id }, pizzaEntity);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var pizzas = await _pizzaStore.GetPizzaAsync();
            if (pizzas.IsSuccess)
            {
                return Ok(pizzas.pizza);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var pizza = await _pizzaStore.GetPizzaAsync(id);
            if (pizza.IsSuccess)
            {
                return Ok(pizza.pizzaResult);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdatedPizza updatedPizza)
        {
            var updatedPizzaResult = await _pizzaStore.UpdatePizzaAsync(id, updatedPizza);
            if (updatedPizzaResult.IsSuccess)
            {
                return Ok(updatedPizzaResult.updatedPizza);
            }
            return BadRequest();
        }
    }
}