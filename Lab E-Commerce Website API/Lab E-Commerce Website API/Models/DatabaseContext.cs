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

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<ItemListing> ItemListings { get; set; } = null!;

        public DbSet<Transaction> Transactions { get; set; } = null!;

        public DbSet<Cart> Carts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.ID);
            modelBuilder.Entity<ItemListing>().HasKey(x => x.ID);
            modelBuilder.Entity<Transaction>().HasKey(x => x.ID);
            modelBuilder.Entity<Cart>().HasKey(x => x.ID);
            base.OnModelCreating(modelBuilder);
        }
    }
}
