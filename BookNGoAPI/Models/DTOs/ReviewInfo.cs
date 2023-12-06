namespace BookNGoAPI.Models.DTOs
{
    public class ReviewInfo
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }
    }
}
