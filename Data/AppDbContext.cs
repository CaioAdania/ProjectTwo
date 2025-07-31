using Microsoft.EntityFrameworkCore;
using ProjectTwo.Models;

namespace ProjectTwo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Itens> Itens { get; set; }
        public DbSet<Clients> Clients { get; set; }
    }
}
