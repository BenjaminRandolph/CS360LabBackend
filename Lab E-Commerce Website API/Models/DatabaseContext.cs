using Microsoft.EntityFrameworkCore;
using Lab_E_Commerce_Website_API.Models;

namespace Lab_E_Commerce_Website_API
{
    // defines our database connection so we can interface with the database
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; } = null!;

        public DbSet<ItemListing> itemlistings { get; set; } = null!;

        public DbSet<Transaction> transactions { get; set; } = null!;

        public DbSet<Cart> carts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.id);
            modelBuilder.Entity<ItemListing>().HasKey(x => x.id);
            modelBuilder.Entity<Transaction>().HasKey(x => x.id);
            modelBuilder.Entity<Cart>().HasKey(x => x.id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
