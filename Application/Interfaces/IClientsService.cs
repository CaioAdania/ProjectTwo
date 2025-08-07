using ProjectTwo.Application.DTOs;
using ProjectTwo.Application.Services;
using ProjectTwo.Entities.Models;

namespace ProjectTwo.Application.Interfaces
{
    public interface IClientsService 
    {
        Task<List<ClientsModel>> GetAllClientsAsync();
        Task<ClientsModel> GetClientByIdAsync(int id);
        Task<ClientsModel> AddClientsAsync(ClientsModel client);
        Task<ClientsModel> UpdateClientAsync(int id, ClientsDTO dto);
        Task<ClientsModel> InactiveClientAsync(int id);
        Task<ClientsModel> ActiveClientAsync(int id);
        Task<ClientsModel> DeleteClientAsync(int id);
    }
}
