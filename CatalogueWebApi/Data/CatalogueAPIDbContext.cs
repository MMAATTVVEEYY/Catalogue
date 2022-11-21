using CatalogueWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogueWebApi.Data
{
    public class CatalogueAPIDbContext:DbContext
    {
        public CatalogueAPIDbContext(DbContextOptions options):base (options)
        {

        }
        public  DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

    }
}
