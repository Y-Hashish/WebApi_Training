using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Training.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet]
        public IActionResult testPrimitive(int age , string name )
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult testGet(Department dept ,string name)
        {
            return Ok();
        }
    }
}
