using ProjectTwo.Application.Interfaces;
using ProjectTwo.Entities.Models;
using ProjectTwo.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectTwo.Application.DTOs;
using System.Text.RegularExpressions;
using ProjectTwo.Entities.Response;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

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
            return await _context.Clients.Where(c => c.IsDeleted != true).ToListAsync();
        }

        public async Task<OperationResult<ClientsModel>> GetClientByIdAsync(int id)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();

            if (idClient == null)
            {
                return result.Fail("Cliente não encontrado.", "Not Found 404");
            }
            else
            {
                return result.Ok(idClient);
            }
        }

        public async Task<OperationResult<ClientsModel>>AddClientsAsync(ClientsModel clients)
        {
            var result = new OperationResult<ClientsModel>();

            try
            {
                _context.Clients.Add(clients);
                await _context.SaveChangesAsync();

                return result.Ok(clients);
            }
            catch
            {
                return result.Fail("Falta campos para serem preenchidos","BadRequest 400");
            }
        }
        public async Task<OperationResult<ClientsModel>> UpdateClientAsync(int id, ClientsDTO dto)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();
            bool hasUpdates = false;
            
            //ideia é refatorar no futuro

            if (idClient == null)
            {
                return result.Fail("Cliente não encontrado.","Not Found 404");
            }

            if (dto.PhoneNumber != null && IsValidPhoneNumber(dto.PhoneNumber))
            {
                idClient.PhoneNumber = dto.PhoneNumber;
                hasUpdates = true;
            }

            if (dto.Email != null)
            {
                idClient.Email = dto.Email;
                hasUpdates = true;
            }

            if (dto.Address != null)
            {
                idClient.Address = dto.Address;
                hasUpdates = true;
            }

            if (dto.City != null)
            {
                idClient.City = dto.City;
                hasUpdates = true;
            }

            if (dto.Number.HasValue)
            {
                idClient.Number = dto.Number.Value;
                hasUpdates = true;
            }

            if (hasUpdates)
            {
                await _context.SaveChangesAsync();
                return result.Ok(idClient);
            }

            return result.Fail("Nenhum campo válido foi fornecido para atualização", "Ok");
        }

        public async Task<OperationResult<ClientsModel>>InactiveClientAsync(int id)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(idClient == null)
            {
                return result.Fail("Cliente não localizado.", "Not Found 404");
            }
            if(idClient.StateCode == false)
            {
                return result.Fail("Não é possivel inativar um cliente já inativo", "Bad Request 400");
            }

            idClient.StateCode = false;
            await _context.SaveChangesAsync();

            return result.Ok(idClient);
    
        }

        public async Task<OperationResult<ClientsModel>> ActiveClientAsync(int id)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (idClient == null)
            {
                return result.Fail("Cliente não localizado", "Not Found 404");
            }
            if(idClient.StateCode == true)
            {
                return result.Fail("Não é possivel ativar um cliente já ativo.", "Bad Request 400");
            }
           
            idClient.StateCode = true;
            await _context.SaveChangesAsync();

            return result.Ok(idClient);

        }

        public async Task<OperationResult<ClientsModel>> DeleteClientAsync(int id)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(idClient == null)
            {
                return result.Fail("Cliente não localizado.", "Not Found 404");
            }
            if(idClient.StateCode == true)
            {
                return result.Fail("Não é possivel deletar um cliente ativo.", "BadRequest 400");
            }

            idClient.IsDeleted = true;
            idClient.DeletedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return result.Ok(idClient);
            
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
