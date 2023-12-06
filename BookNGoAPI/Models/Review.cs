using System.ComponentModel.DataAnnotations.Schema;

namespace BookNGoAPI.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string UserGuid { get; set; }
        [ForeignKey("Hotel")]
        [Column(Order = 1)]
        public int HotelId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }
        public Hotel? Hotel { get; set; }
        public User? User { get; set; }
    }
}
