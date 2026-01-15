using Microsoft.AspNetCore.Mvc;

namespace SurgeryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Hello from SurgeryApp working API Test !!! ");

    }
}
