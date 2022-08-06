using Microsoft.EntityFrameworkCore;

namespace ParkyWebAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalPark> NationalPark { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
