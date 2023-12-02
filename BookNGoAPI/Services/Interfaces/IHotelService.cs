using BookNGoAPI.Models;

namespace BookNGoAPI.Services.Interfaces
{
    public interface IHotelService
    {
        Hotel FindHotelById(int id);
    }
}
