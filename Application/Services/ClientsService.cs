using ProjectTwo.Application.Interfaces;
using ProjectTwo.Entities.Models;
using ProjectTwo.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectTwo.Application.DTOs;
using System.Text.RegularExpressions;

namespace ProjectTwo.Application.Services
{
    public class ClientsService : IClientsService
    {
        private readonly AppDbContext _context;

        public ClientsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ClientsModel> GetClientByIdAsync(int id)
        {
            var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();

            if (idClient == null)
            {
                throw new ArgumentException("Cliente não localizado.");
            }

            return idClient;
        }
        public async Task<List<ClientsModel>> GetAllClientsAsync()
        {
            return await _context.Clients.Where(c => c.IsDeleted != true).ToListAsync();
        }

        public async Task<ClientsModel> AddClientsAsync(ClientsModel clients)
        {
            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return clients;
        }
        public async Task<ClientsModel> UpdateClientAsync(int id, ClientsDTO dto)
        {
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            //ideia é refatorar no futuro
            if(idClient == null)
            {
                throw new KeyNotFoundException($"Cliente não foi localizado pelo Id: {id}");
            }
            if(IsValidPhoneNumber(dto.PhoneNumber)) //melhorar
            {
                idClient.PhoneNumber = dto.PhoneNumber;
            }
            if(dto.Email != null)
            {
                idClient.Email = dto.Email;
            }
            if(dto.Address != null)
            {
                idClient.Address = dto.Address;
            }
            if(dto.City != null)
            {
                idClient.City = dto.City;
            }
            if (dto.Number.HasValue)
            {
                idClient.Number = dto.Number.Value;
            }

            await _context.SaveChangesAsync();

            return idClient;
        }

        public async Task<ClientsModel> InactiveClientAsync(int id)
        {
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(idClient == null)
            {
                throw new KeyNotFoundException($"Cliente não foi localizado pelo Id: {id}"); //tratar todos throw
            }

            idClient.StateCode = false;
            await _context.SaveChangesAsync();

            return idClient;
        }

        public async Task<ClientsModel> ActiveClientAsync(int id)
        {
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (idClient == null)
            {
                throw new KeyNotFoundException($"Cliente não foi localizado pelo Id: {id}"); //tratar todos throw
            }

            idClient.StateCode = true;
            await _context.SaveChangesAsync();

            return idClient;
        }

        public async Task<ClientsModel> DeleteClientAsync(int id)
        {
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(idClient == null)
            {
                throw new ArgumentException($"Cliente não foi localizado pelo Id: {id}"); //tratar todos throw
            }
            if(idClient.StateCode == true)
            {
                throw new ArgumentException($"Não é possivel deletar um cliente ativo"); //tratar todos throw
            }

            idClient.IsDeleted = true;
            idClient.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return idClient;
        }
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^[\d\s\-\(\)\+]+$");
            if (!regex.IsMatch(phoneNumber) || phoneNumber == null || phoneNumber.Length <= 9)
            {
                return false;
            }

            return true;
        }
    }
}
