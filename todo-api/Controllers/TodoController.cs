using ASPNetCoreMastersTodoList.Api.BindingModels;
using Microsoft.AspNetCore.Mvc;
using todo_api.Data;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly DataContext dataContext;

        public TodoController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Returns all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var todos = this.dataContext.Todos.OrderByDescending(_ => _.Id);

            return Ok(todos);
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
            var todo = this.dataContext.Todos.FirstOrDefault(todo => todo.Id == itemId);

            return Ok(todo);
        }

        /// <summary>
        /// Adds a new item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] CreateTodoModel model)
        {
            if (!ModelState.IsValid)
			{
                return BadRequest(ModelState);
			}

            this.dataContext.Todos.Add(new Todo
            {
                Text = model.Text,
                Category = model.Category,
                TargetDate = model.TargetDate,
                DateCreated = DateTime.Now
            });

            this.dataContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates an existing item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{itemId}")]
        public IActionResult Put(int itemId, [FromBody] UpdateTodoModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = this.dataContext.Todos.FirstOrDefault(_ => _.Id == itemId);

            if (todo is null) return NotFound(model);

            todo.Text = model.Text;
            todo.Category = model.Category;
            todo.Completed = model.Completed;
            todo.TargetDate = model.TargetDate;

            this.dataContext.SaveChanges();

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
            var todo = this.dataContext.Todos.FirstOrDefault(_ => _.Id == itemId);

            if (todo is null) return NotFound(itemId);

            this.dataContext.Remove(todo);

            this.dataContext.SaveChanges();

            return Ok();
        }
    }
}
