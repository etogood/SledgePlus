using Microsoft.EntityFrameworkCore;

namespace SledgePlus.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=de2000.iaasdns.com;user=ilya;password=krh_xwj3rjx3HYX.yjb;database=ilya_SledgePlus;");
        }
    }
}
