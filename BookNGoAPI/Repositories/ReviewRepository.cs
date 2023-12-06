using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;

namespace BookNGoAPI.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
