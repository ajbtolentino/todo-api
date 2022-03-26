using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error() => Problem();
    }

}
