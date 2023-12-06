using System.ComponentModel.DataAnnotations.Schema;

namespace BookNGoAPI.Models
{
    public class BookFlight
    {
        public int BookFlightId { get; set; }
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string UserGuid { get; set; }
        [ForeignKey("Flight")]
        [Column(Order = 1)]
        public int FlightId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Flight? Flight { get; set; }
        public User? User { get; set; }
    }
}
