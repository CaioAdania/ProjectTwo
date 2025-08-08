using ProjectTwo.Application.DTOs;
using ProjectTwo.Application.Services;
using ProjectTwo.Entities.Models;
using ProjectTwo.Entities.Response;

namespace ProjectTwo.Application.Interfaces
{
    public interface IClientsService 
    {
        Task<List<ClientsModel>> GetAllClientsAsync();
        Task<OperationResult<ClientsModel>> GetClientByIdAsync(int id);
        Task<OperationResult<ClientsModel>> AddClientsAsync(ClientsModel client);
        Task<OperationResult<ClientsModel>> UpdateClientAsync(int id, ClientsDTO dto);
        Task<OperationResult<ClientsModel>> InactiveClientAsync(int id);
        Task<OperationResult<ClientsModel>> ActiveClientAsync(int id);
        Task<OperationResult<ClientsModel>> DeleteClientAsync(int id);
    }
}
