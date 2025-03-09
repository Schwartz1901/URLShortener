using Microsoft.EntityFrameworkCore;

namespace URLShortener
{
    public class UrlDbContext : DbContext
    {
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }
        public DbSet<UrlEntry> Urls { get; set; }
    }
}
