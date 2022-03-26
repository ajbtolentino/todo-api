using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using todo_api.Service;

namespace todo_api.Filters
{
    public class TodoExistsFilter : ActionFilterAttribute
    {
        private const string ARGUMENT_ITEM_ID = "itemId";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var itemService = context.HttpContext.RequestServices.GetService<ITodoService>();
            var containsItemId = context.ActionArguments.ContainsKey(TodoExistsFilter.ARGUMENT_ITEM_ID);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (containsItemId &&
                int.TryParse(context.ActionArguments[ARGUMENT_ITEM_ID].ToString(), out int itemId))
            {
                if(itemService is not null) if(itemService.Get(itemId) is null) context.Result = new NotFoundResult();
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}

