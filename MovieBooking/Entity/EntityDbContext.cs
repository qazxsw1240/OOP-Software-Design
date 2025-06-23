using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieBooking.Entity
{
    public class EntityDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<ShowTime> ShowTimes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=movies.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Booking>().ToTable("Bookings");
            modelBuilder.Entity<ShowTime>().ToTable("ShowTimes");
        }
    }

    public class EntityDbContextFactory : IDesignTimeDbContextFactory<EntityDbContext>
    {
        public EntityDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<EntityDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlite("Data Source=movies.db");
            return new(optionsBuilder.Options);
        }
    }
}
