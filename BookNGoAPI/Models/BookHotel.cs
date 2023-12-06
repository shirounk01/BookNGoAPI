using System.ComponentModel.DataAnnotations.Schema;

namespace BookNGoAPI.Models
{
    public class BookHotel
    {
        public int BookHotelId { get; set; }
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string UserGuid { get; set; }
        [ForeignKey("Hotel")]
        [Column(Order = 1)]
        public int HotelId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Price { get; set; }
        public Hotel? Hotel { get; set; }
        public User? User { get; set; }
    }
}
