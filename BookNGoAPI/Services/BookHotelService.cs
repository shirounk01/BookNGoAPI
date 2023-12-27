using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;

namespace BookNGoAPI.Services
{
    public class BookHotelService : IBookHotelService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IHotelService _hotelService;
        private readonly IUserService _userService;

        public BookHotelService(IRepositoryWrapper repo, IHotelService hotelService, IUserService userService)
        {
            _repo = repo;
            _hotelService = hotelService;
            _userService = userService;
        }

        public void BookHotel(int id, Hotel reference)
        {
            Hotel hotel = _hotelService.FindHotelById(id);

            var userGuid = _userService.GetGuid();
            BookHotel bookHotel = BuildNewBookHotel(userGuid, reference, hotel);

            _repo.HotelRepository.Update(hotel);
            _repo.BookHotelRepository.Create(bookHotel);

            _repo.Save();
        }

        private static BookHotel BuildNewBookHotel(string userGuid, Hotel reference, Hotel hotel)
        {
            BookHotel bookHotel = new BookHotel();
            bookHotel.UserGuid = userGuid;
            bookHotel.Hotel = hotel;
            bookHotel.RegistrationDate = DateTime.Now;
            bookHotel.CheckInDate = reference.OpenDate;
            bookHotel.CheckOutDate = reference.CloseDate;
            bookHotel.Price = hotel.Price * ((int)(reference.CloseDate - reference.OpenDate).TotalDays);
            return bookHotel;
        }

        public List<BookHotel> GetReservationsByUserId(string userGuid)
        {
            List<BookHotel> hotelReservations = _repo.BookHotelRepository.FindByCondition(hotel => hotel.UserGuid == userGuid).ToList();
            return hotelReservations;
        }
    }

}
