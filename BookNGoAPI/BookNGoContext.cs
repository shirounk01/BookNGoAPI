using BookNGoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookNGoAPI
{
    public class BookNGoContext:DbContext
    {
        public BookNGoContext(DbContextOptions<BookNGoContext> options) : base(options) { }
        public DbSet<Hotel>? Hotels { get; set; }

    }
}
