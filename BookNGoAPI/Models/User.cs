using System.ComponentModel.DataAnnotations;

namespace BookNGoAPI.Models
{
    public class User
    {
        [Key]
        public string UserGuid { get; set; } = Guid.NewGuid().ToString();
        public string? Email { get; set; }
        public string? Password { get; set; }
        public ICollection<BookFlight>? BookedFlights { get; set; }
        public ICollection<BookHotel>? BookedHotels { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
