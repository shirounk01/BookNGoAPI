using BookNGoAPI.Models;

namespace BookNGoAPI.Services.Interfaces
{
    public interface IBookHotelService
    {
        void BookHotel(int id, string userGuid, Hotel reference);
        List<BookHotel> GetReservationsByUserId(string userGuid);
    }
}
