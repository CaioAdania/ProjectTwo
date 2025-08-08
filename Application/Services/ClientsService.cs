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
                result.ErrorMessage = "Cliente não localizado.";
                result.Success = false;

                return result;
            }
            else
            {
                return result.Ok(idClient);
            }
        }

        public async Task<OperationResult<ClientsModel>>AddClientsAsync(ClientsModel clients)
        {
            var result = new OperationResult<ClientsModel>();

            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return result;
        }
        public async Task<OperationResult<ClientsModel>> UpdateClientAsync(int id, ClientsDTO dto)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            //ideia é refatorar no futuro
            if(idClient == null)
            {
                return result.Fail(errorMessage: "Cliente não encontrado.", errorType: "Not Found 404");
            }
            else if(dto.PhoneNumber != null) //melhorar
            {
                if (!IsValidPhoneNumber(dto.PhoneNumber))
                {
                    idClient.PhoneNumber = dto.PhoneNumber;
                }
            }
            else if(dto.Email != null)
            {
                idClient.Email = dto.Email;
            }
            else if(dto.Address != null)
            {
                idClient.Address = dto.Address;
            }
            else if(dto.City != null)
            {
                idClient.City = dto.City;
            }
            else if (dto.Number.HasValue)
            {
                idClient.Number = dto.Number.Value;
            }
            else
            {
                await _context.SaveChangesAsync();

                return result.Ok(idClient);
            }
        }

        public async Task<OperationResult<ClientsModel>>InactiveClientAsync(int id)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(idClient == null)
            {
                return result.Fail(errorMessage: "Cliente não localizado.", errorType: "Not Found 404");
            }
            else
            {
                idClient.StateCode = false;
                await _context.SaveChangesAsync();

                return result.Ok(idClient);
            }        
        }

        public async Task<OperationResult<ClientsModel>> ActiveClientAsync(int id)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (idClient == null)
            {
                return result.Fail(errorMessage: "Cliente não localizado", errorType: "Not Found 404");
            }
            if(idClient.StateCode == true)
            {
                return result.Fail(errorMessage: "Não é possivel ativar um cliente já ativo.", errorType: "Ok 200");
            }
            else
            {
                idClient.StateCode = true;
                await _context.SaveChangesAsync();

                return result.Ok(idClient);
            }
        }

        public async Task<OperationResult<ClientsModel>> DeleteClientAsync(int id)
        {
            var result = new OperationResult<ClientsModel>();
            var idClient = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(idClient == null)
            {
                return result.Fail(errorMessage: "Cliente não localizado.", errorType: "Not Found 404");
            }
            if(idClient.StateCode == true)
            {
                return result.Fail(errorMessage: "Não é possivel deletar um cliente ativo.", errorType: "Ok 200");
            }
            else
            {
                idClient.IsDeleted = true;
                idClient.DeletedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return result.Ok(idClient);
            }
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
