using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using todo_service;

namespace ASPNetCoreMastersTodoList.Api.Filters
{
    public class ItemExistsFilter : ActionFilterAttribute
    {
        private const string ARGUMENT_ITEM_ID = "itemId";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var itemService = context.HttpContext.RequestServices.GetService<IItemService>();
            var containsItemId = context.ActionArguments.ContainsKey(ItemExistsFilter.ARGUMENT_ITEM_ID);

            if (containsItemId)
            {
                var itemId = (int)context.ActionArguments[ARGUMENT_ITEM_ID];

                if(itemService.Get(itemId) is null) context.Result = new NotFoundResult();
            }
        }
    }
}

