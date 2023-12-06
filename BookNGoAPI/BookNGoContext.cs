using BookNGoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookNGoAPI
{
    public class BookNGoContext:DbContext
    {
        public BookNGoContext(DbContextOptions<BookNGoContext> options) : base(options) { }
        public DbSet<Review>? Reviews { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }
        public DbSet<Flight>? Flights { get; set; }
        public DbSet<BookHotel>? BookHotels { get; set; }
        public DbSet<BookFlight>? BookFlights { get; set; }
        public DbSet<User>? Users { get; set; }

    }
}
