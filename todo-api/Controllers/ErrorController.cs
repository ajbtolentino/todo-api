using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]
        public IActionResult Error() => Problem();
    }

}
