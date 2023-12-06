using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;

namespace BookNGoAPI.Repositories
{
    public class BookHotelRepository : RepositoryBase<BookHotel>, IBookHotelRepository
    {
        public BookHotelRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
