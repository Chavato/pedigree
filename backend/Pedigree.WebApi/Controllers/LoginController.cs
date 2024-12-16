using Microsoft.AspNetCore.Mvc;

namespace Pedigree.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : DefaultController
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Sucessfully");
        }
    }
}