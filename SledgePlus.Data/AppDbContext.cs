using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SledgePlus.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Section> Sections { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=de2000.iaasdns.com;user=ilya_SledgePlusAdmin;password={1nXF@8vyoSC;database=ilya_SledgePlus;Charset=utf8;");
        }

        // ilya_SledgePlusAdmin : {1nXF@8vyoSC
        // ilya_SledgePlusUser  : eyhMLsWrw#^d
    }
}