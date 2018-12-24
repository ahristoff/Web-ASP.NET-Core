
namespace News.Data
{
    using Microsoft.EntityFrameworkCore;
    using News.Data.Models;

    public class NewsDbContext: DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            :base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
