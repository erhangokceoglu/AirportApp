using AirportDistanceRestApi.Entities;
using AirportDistanceRestApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportDistanceRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportDistanceController : ControllerBase
    {
        private readonly IAirportDistanceService _airportDistanceService;

        public AirportDistanceController(IAirportDistanceService airportDistanceService)
        {
            _airportDistanceService = airportDistanceService;
        }

        [HttpGet("Calculate-In-Miles")]
        public async Task<IActionResult> Calculate([FromQuery]IataCode iataCode)
        {
            return Ok(await _airportDistanceService.Calculate(iataCode));
        }
    }
}
