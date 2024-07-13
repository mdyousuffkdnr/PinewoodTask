
using Microsoft.EntityFrameworkCore;
using Pinewood.Api.Models;

namespace Pinewood.Api.DataAccess
{
    public class AppDbContextInMemory(DbContextOptions<AppDbContextInMemory> options) : DbContext(options)
    {

        public DbSet<Customer> Customers { get; set; }
    }
}
