using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Flights.Server.Dtos;

namespace Flights.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
