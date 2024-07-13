using Microsoft.EntityFrameworkCore;
using Pinewood.Api.Models;

namespace Pinewood.Api.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
