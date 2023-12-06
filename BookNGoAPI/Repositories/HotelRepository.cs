using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;

namespace BookNGoAPI.Repositories
{
    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }

        public List<Hotel> FindByModel(Hotel hotel)
        {
            var result = Context.Hotels!.AsQueryable();
            result = result.Where(item => item.City == hotel.City);
            return result.ToList();
        }
    }
}
