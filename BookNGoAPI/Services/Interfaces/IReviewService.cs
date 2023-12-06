using BookNGoAPI.Models;

namespace BookNGoAPI.Services.Interfaces
{
    public interface IReviewService
    {
        List<Review> FindReviewsByHotelId(int id);
        void AddReview(Review review, int id, string userGuid);
    }
}
