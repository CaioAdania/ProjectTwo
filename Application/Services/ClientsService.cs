using ProjectTwo.Application.Interfaces;
using ProjectTwo.Entities.Models;
using ProjectTwo.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectTwo.Application.Services
{
    public class ClientsService : IClientsService
    {
        private readonly AppDbContext _context;

        public ClientsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientsModel>> GetAllClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<ClientsModel> AddClientsAsync(ClientsModel clients)
        {
            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return clients;
        }
    }
}
