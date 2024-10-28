using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedisAndUoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok("Im fine");
        }
    }
}
