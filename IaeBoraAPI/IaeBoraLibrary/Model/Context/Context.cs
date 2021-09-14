using Microsoft.EntityFrameworkCore;

namespace IaeBoraLibrary.Model.Context
{
    public class Context : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Route> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>().Ignore(r => r.RouteCategories);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LocalHost\SQLSERVER;Database=iaeboradb;User Id=sa;Password=123456;Trusted_Connection=true;");
        }
    }
}
