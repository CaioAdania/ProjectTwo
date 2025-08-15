using Microsoft.AspNetCore.Mvc;
using ProjectTwo.Entities.Models;
using ProjectTwo.Entities.Response;

namespace ProjectTwo.Application.Interfaces
{
    public interface IItensService
    {
        Task<List<ItensModel>> GetAllItensAsync();
        Task<OperationResult<ItensModel>> GetItemByIdAsync(int id);
        Task<OperationResult<ItensModel>> AddItensAsync(ItensModel itens);
        Task<OperationResult<ItensModel>> InactiveItemAsync(int id);
        Task<OperationResult<ItensModel>> ActiveItemAsync(int id);
        Task<OperationResult<ItensModel>> DeleteItemAsync(int id);
        Task<OperationResult<ItensModel>> RemoveAmountItemAsync(int id, int amount);
        Task<OperationResult<ItensModel>> AddAmountItemAsync(int id, int amount);
    }
}
