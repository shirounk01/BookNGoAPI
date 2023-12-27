using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Services.Interfaces;

namespace BookNGoAPI.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;
        private readonly IBookHotelService _bookHotelService;
        private readonly IBookFlightService _bookFlightService;
        private readonly IHotelService _hotelService;
        private readonly IFlightService _flightService;

        public ProfileService(IUserService userService, IBookHotelService bookHotelService, IBookFlightService bookFlightService, IHotelService hotelService, IFlightService flightService)
        {
            _userService = userService;
            _bookHotelService = bookHotelService;
            _bookFlightService = bookFlightService;
            _hotelService = hotelService;
            _flightService = flightService;
        }

        public History GetProfile()
        {
            var userGuid = _userService.GetGuid();
            var bookedHotels = _bookHotelService.GetReservationsByUserId(userGuid);
            var bookedFlights = _bookFlightService.GetReservationsByUserId(userGuid);
            List<Tuple<BookHotel, Hotel>> hotelHistory = _hotelService.GetHotelsByReservations(bookedHotels);
            List<Tuple<BookFlight, Flight>> flightHistory = _flightService.GetHotelsByReservations(bookedFlights);
            History history = new History();
            history.Hotels = hotelHistory;
            history.Flights = flightHistory;
            return history;
        }
    }
}
