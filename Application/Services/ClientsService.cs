using ProjectTwo.Application.Interfaces;
using ProjectTwo.Entities.Models;
using ProjectTwo.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

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
        public async Task<ClientsModel> UpdateAddressUserAsync(int id, string address)
        {
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(idClient == null)
            {
                throw new KeyNotFoundException($"Cliente não foi localizado pelo Id: {id}");
            }

            idClient.Address = address;
            await _context.SaveChangesAsync();

            return idClient;
        }
    }
}
