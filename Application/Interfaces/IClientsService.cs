using ProjectTwo.Application.Services;
using ProjectTwo.Entities.Models;

namespace ProjectTwo.Application.Interfaces
{
    public interface IClientsService 
    {
        Task<List<ClientsModel>> GetAllClientsAsync();
        Task<ClientsModel> AddClientsAsync(ClientsModel client);
        Task<ClientsModel> UpdateAddressUserAsync(int id, string address);
    }
}
