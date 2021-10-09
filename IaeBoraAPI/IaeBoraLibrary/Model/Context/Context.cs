using Microsoft.EntityFrameworkCore;

namespace IaeBoraLibrary.Model.Context
{
    public class Context : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TouristPoint> TouristPoints { get; set; }

        public DbSet<Place> Place { get; set; }
        public DbSet<Opening_Hours> Opening_Hours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>().Ignore(r => r.RouteCategories);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseSqlServer(@"Server=LocalHost\SQLSERVER;Database=iaebora;User Id=sa;Password=123456;");
#else
            optionsBuilder.UseSqlServer(@"Server=34.85.205.224;Database=iaebora;User Id=sqlserver;Password=Iae Bora 123456;");
#endif
        }
    }
}
