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
        /// Returns all todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var todos = this.dataContext.Todos.OrderByDescending(_ => _.Id);

            return Ok(todos);
        }

        /// <summary>
        /// Returns a todo with the given id
        /// </summary>
        /// <param name="todoId">Id of the item</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{todoId}")]
        public IActionResult Get(int todoId)
        {
            var todo = this.dataContext.Todos.FirstOrDefault(todo => todo.Id == todoId);

            return Ok(todo);
        }

        /// <summary>
        /// Adds a new todo
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
        /// Updates an existing todo
        /// </summary>
        /// <param name="todoId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{itemId}")]
        public IActionResult Put(int todoId, [FromBody] UpdateTodoModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = this.dataContext.Todos.FirstOrDefault(_ => _.Id == todoId);

            if (todo is null) return NotFound(model);

            todo.Text = model.Text;
            todo.Category = model.Category;
            todo.Completed = model.Completed;
            todo.TargetDate = model.TargetDate;

            this.dataContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes an todo
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{todoId}")]
        public IActionResult Delete(int todoId)
        {
            var todo = this.dataContext.Todos.FirstOrDefault(_ => _.Id == todoId);

            if (todo is null) return NotFound(todoId);

            this.dataContext.Remove(todo);

            this.dataContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Toggles completed state of an existing todo
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("toggleCompleted/{todoId}")]
        public IActionResult ToggleCompleted(int todoId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = this.dataContext.Todos.FirstOrDefault(_ => _.Id == todoId);

            if (todo is null) return NotFound(todoId);

            todo.Completed = !todo.Completed;

            this.dataContext.SaveChanges();

            return Ok();
        }
    }
}
