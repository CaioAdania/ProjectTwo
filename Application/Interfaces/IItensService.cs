using Microsoft.AspNetCore.Mvc;
using ProjectTwo.Entities.Models;
using ProjectTwo.Entities.Response;

namespace ProjectTwo.Application.Interfaces
{
    public interface IItensService
    {
        Task<List<ItensModel>> GetAllItensAsync();
        Task<OperationResult<ItensModel>> GetItemByIdAsync(int id);

    }
}
