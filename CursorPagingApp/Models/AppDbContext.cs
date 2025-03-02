using Microsoft.EntityFrameworkCore;

namespace CursorPagingApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(SeedData());
        }

        private static List<Product>SeedData()
        {
            var products = new List<Product>();
            var categories = new[] { "Electronics", "Clothing", "Toys" }; 
            for (int i = 1; i <= 100; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    Category = categories[i % categories.Length]
                });
            }

            return products;
        }
    }
}
