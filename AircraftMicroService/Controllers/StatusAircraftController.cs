using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AircraftMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusAircraftController : ControllerBase
    {
        [HttpGet]
        public IActionResult StatusApiAircraft()
        {
            return Ok();
        }
    }
}
