using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;

namespace BookNGoAPI.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepositoryWrapper _repo;

        public HotelService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public Hotel FindHotelById(int id)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault();
            return hotel!;
        }
    }
}
