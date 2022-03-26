using ASPNetCoreMastersTodoList.Api.BindingModels;
using ASPNetCoreMastersTodoList.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using todo_service;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ItemExistsFilter]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService itemService;
        private readonly ILogger logger;

        public ItemsController(IItemService itemService, ILogger<ItemsController> logger)
        {
            this.itemService = itemService;
            this.logger = logger;
        }

        /// <summary>
        /// Returns all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this.itemService.GetAll();

            logger.LogInformation("Items retrieved: {@ItemCount}", result.Count());

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

            logger.LogInformation("Item retrieved: {@Item}", result);

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

            this.itemService.Add(new ItemDTO
            {
                Text = itemCreateModel.Text,
                DateCreated = DateTime.UtcNow
            });

            this.logger.LogInformation("Item Added: {@Payload}", itemCreateModel);

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

            this.itemService.Update(new ItemDTO
            {
                Id = itemId,
                Text = itemUpdateModel.Text
            });

            this.logger.LogInformation("Item updated: {@Payload}", itemUpdateModel);

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

            this.logger.LogInformation("Item deleted: {ItemId}", itemId);

            return Ok();
        }
    }
}
