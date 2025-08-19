using Microsoft.EntityFrameworkCore;
using ProjectTwo.Application.Interfaces;
using ProjectTwo.Entities.Models;

namespace ProjectTwo.Infrastruture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ItensModel> Itens { get; set; }
        public DbSet<ClientsModel> Clients { get; set; }
        public DbSet<MembersModel> Members { get; set; }
        public DbSet<ProfileTypeMemberModel> ProfileTypes { get; set; }
    }
}
