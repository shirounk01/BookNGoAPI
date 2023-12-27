using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;

namespace BookNGoAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IUserService _userService;

        public ReviewService(IRepositoryWrapper repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;
        }

        public void AddReview(Review review, int id)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault();
            review.Hotel = hotel;
            review.UserGuid = _userService.GetGuid();
            review.Created = DateTime.Now;
            _repo.ReviewRepository.Create(review);
            _repo.HotelRepository.Update(hotel!);
            _repo.Save();
        }

        public List<Review> FindReviewsByHotelId(int id)
        {
            var reviews = _repo.ReviewRepository.FindByCondition(item => item.HotelId == id).OrderByDescending(item => item.Created).ToList();
            return reviews;
        }
    }

}
