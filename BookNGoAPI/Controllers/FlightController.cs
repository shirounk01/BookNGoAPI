using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookNGoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IFlightPackService _flightPackService;
        private readonly IBookFlightService _bookFlightService;
        private readonly IUserService _userService;

        public FlightController(IFlightService flightService, IFlightPackService flightPackService, IBookFlightService bookFlightService, IUserService userService)
        {
            _flightService = flightService;
            _flightPackService = flightPackService;
            _bookFlightService = bookFlightService;
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Index(FlightInfo flightInfo)
        {
            Flight flight = new() { DepartureCity = flightInfo.DepartureCity, ArrivalCity = flightInfo.ArrivalCity, DepartureTime = flightInfo.DepartureTime, ArrivalTime = flightInfo.ArrivalTime };
            List<Flight> flightsGoing = _flightService.GetFlightsByModel(flight);
            List<Flight> flightsComing = _flightService.GetFlightsByModel(_flightService.SwapRoute(flight));
            var flightPack = _flightPackService.GeneratePack(flightsGoing, flightsComing);
            var returnObj = new { Flight = flight, FlightPack = flightPack };
            return Ok(returnObj);
        }
        [HttpPost("Filter")]
        public IActionResult IndexFiltered([FromQuery] Filter filter, [FromBody] List<FlightPack> flights)
        {
            var updatedFlights = _flightPackService.FilterFlights(filter, flights!);
            return Ok(updatedFlights);
        }
        [Authorize]
        [HttpPost("Book/{goingId}-{comingId}")]
        public IActionResult BookFlight(int goingId, int comingId)
        {
            if(_bookFlightService.CheckContinuity(goingId, comingId))
            {
                _bookFlightService.BookFlights(goingId, comingId);
                return Ok();
            }
            return BadRequest();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Flight flight)
        {
            _flightService.AddFlights(flight);
            return Ok();
        }
    }
}
