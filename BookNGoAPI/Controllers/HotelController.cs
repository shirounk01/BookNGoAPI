using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BookNGoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public readonly IBookHotelService _bookHotelService;
        public readonly IReviewService _reviewService;

        public HotelController(IHotelService hotelService, IBookHotelService bookHotelService, IReviewService reviewService, IUserService userService)
        {
            _hotelService = hotelService;
            _bookHotelService = bookHotelService;
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult Index([FromBody] Hotel hotel)
        {
            var results = _hotelService.GetHotelsByModel(hotel);
            return Ok(results);
        }
        [HttpPost("Filter")]
        public IActionResult IndexFiltered([FromQuery] Filter filter, [FromBody] List<Hotel> hotels)
        {
            var updatedHotels = _hotelService.FilterHotels(filter, hotels!);
            return Ok(updatedHotels);

        }

        // manage the guid
        [Authorize]
        [HttpPost("Book/{id}")]
        public IActionResult BookHotel(int id, [FromBody] HotelInfo hotelInfo)
        {
            var hotel = new Hotel() { OpenDate = hotelInfo.From, CloseDate = hotelInfo.To };
            _bookHotelService.BookHotel(id, hotel!);
            return Ok();
        }
        [HttpGet("Reviews/{id}")]
        public IActionResult Review(int id)
        {
            Hotel hotel = _hotelService.FindHotelById(id);
            List<Review> reviews = _reviewService.FindReviewsByHotelId(id);
            var returnObj = new { Hotel = hotel, Posts = reviews };

            return Ok(returnObj);
        }
        //manage the guid
        [Authorize]
        [HttpPost("Reviews/Post/{id}")]
        public IActionResult Review([FromBody] ReviewInfo reviewInfo, int id, string userGuid)
        {
            var review = new Review() { Comment = reviewInfo.Comment, Created = reviewInfo.Created, Rating = reviewInfo.Rating };
            _reviewService.AddReview(review, id, userGuid);
            return Ok();
        }

    }
}
