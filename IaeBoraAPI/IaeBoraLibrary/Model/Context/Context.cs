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

            //modelBuilder.Entity<Opening_Hours>().ToTable("opening_hours", o => o.ExcludeFromMigrations());
            //modelBuilder.Entity<Place>().ToTable("place", p => p.ExcludeFromMigrations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=34.95.239.134;Database=iaebora;User Id=sqlserver;Password=Iaebora123456;");

            optionsBuilder.UseSqlServer(@"Server=LocalHost\SQLSERVER;Database=iaebora;User Id=sa;Password=123456;"); //Trusted_Connection=true;

            //optionsBuilder.UseSqlServer(@"Server=LocalHost\SQLSERVER;Database=iaeboradev;User Id=sa;Password=123456;"); //Trusted_Connection=true;
        }
    }
}
