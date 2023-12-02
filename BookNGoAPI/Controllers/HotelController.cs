
using BookNGoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookNGoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var hotel = _hotelService.FindHotelById(id);
            return Ok(hotel);
        }
    }
}
