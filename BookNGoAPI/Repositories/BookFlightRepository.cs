using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;

namespace BookNGoAPI.Repositories
{
    public class BookFlightRepository : RepositoryBase<BookFlight>, IBookFlightRepository
    {
        public BookFlightRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
