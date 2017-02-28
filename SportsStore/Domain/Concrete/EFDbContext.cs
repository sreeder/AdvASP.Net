
using System.Data.Entity;
using Domain.Entities;

namespace Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet <Product> Products { get; set; }
    }
    
}
