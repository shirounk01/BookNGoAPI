using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;

namespace BookNGoAPI.Repositories
{
    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
