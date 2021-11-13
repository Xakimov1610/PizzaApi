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
        public async Pizza<IActionResult> CreateTask([FromBody]NewPizza newTask)
        {
            var pizzaEntity = NewPizza.ToTaskEntity();
            var insertResult = await _storage.InsertTaskAsync(pizzaEntity);

            if(insertResult.IsSuccess)
            {
                return CreatedAtAction("CreateTask", pizzaEntity);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = insertResult.exception.Message });
        }

        [HttpGet]
        public async Task<IActionResult> QueryTasks([FromQuery]PizzaQuery query)
        {
            var tasks = await _storage.GetPizzaAsync(title: query.Title, id: query.Id);

            if(tasks.Any())
            {
                return Ok(tasks);
            }

            return NotFound("No tasks exist!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskAsync([FromBody]UpdatePizza updatedPizza)
        {
            var entity = updatedPizza.ToTaskEntity();
            var updateResult = await _storage.UpdatePizzaAsync(entity);

            if(updateResult.isSuccess)
            {
                return Ok();
            }

            return BadRequest(updateResult.exception.Message);
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