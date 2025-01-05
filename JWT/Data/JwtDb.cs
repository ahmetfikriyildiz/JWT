using JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    public class JwtDb
    {
        public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        }
    }
}
