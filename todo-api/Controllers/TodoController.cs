using ASPNetCoreMastersTodoList.Api.BindingModels;
using Microsoft.AspNetCore.Mvc;
using todo_api.Filters;
using todo_api.Service;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [TodoExistsFilter]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService itemService;

        public TodoController(ITodoService itemService)
        {
            this.itemService = itemService;
        }

        /// <summary>
        /// Returns all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this.itemService.GetAll().OrderByDescending(_ => _.Id);

            return Ok(result);
        }

        /// <summary>
        /// Returns an item with the given id
        /// </summary>
        /// <param name="itemId">Id of the item</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{itemId}")]
        public IActionResult Get(int itemId)
        {
            var result = this.itemService.Get(itemId);

            return Ok(result);
        }

        /// <summary>
        /// Adds a new item
        /// </summary>
        /// <param name="itemCreateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ItemCreateBindingModel itemCreateModel)
        {
            if (!ModelState.IsValid)
			{
                return BadRequest(ModelState);
			}

            this.itemService.Add(new TodoDTO
            {
                Text = itemCreateModel.Text,
                DateCreated = DateTime.UtcNow
            });

            return Ok();
        }

        /// <summary>
        /// Updates an existing item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemUpdateModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{itemId}")]
        public IActionResult Put(int itemId, [FromBody] ItemUpdateBindingModel itemUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.itemService.Update(new TodoDTO
            {
                Id = itemId,
                Text = itemUpdateModel.Text
            });

            return Ok();
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{itemId}")]
        public IActionResult Delete(int itemId)
        {
            this.itemService.Delete(itemId);

            return Ok();
        }
    }
}
