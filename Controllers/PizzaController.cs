using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaApi.Model;
using PizzaApi.Service;
using PizzaApi.Entities;

namespace PizzaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IStorageService _storage;

        public PizzaController(ILogger<PizzaController> logger, IStorageService storage)
        {
            _logger = logger;
            _storage = storage;
        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateTask([FromBody]NewPizza newPizza)
        {
            var pizzaEntity = NewPizza.ToPizzaEntity();
            var pizzaResult = await _pizzaStore.InsertPizzaAsync(pizzaEntity);

            if(pizzaResult.IsSuccess)
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

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> RemovePizzaAsync([FromRoute]Guid Id)
        {
            
            var DelateId = await _storage.RemovePizzaAsync(Id);

            if(DelateId.IsSuccess)
            {
                return Ok();
            }

            return NotFound(DelateId.exception.Message);
        }


    }
}
}