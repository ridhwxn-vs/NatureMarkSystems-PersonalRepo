using Microsoft.EntityFrameworkCore;
using ProductInventoryAPI.Models;

namespace ProductInventoryAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
    }
}
