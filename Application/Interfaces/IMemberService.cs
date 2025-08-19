using ProjectTwo.Entities.Models;
using ProjectTwo.Entities.Response;
using ProjectTwo.Application.DTOs;

namespace ProjectTwo.Application.Interfaces
{
    public interface IMemberService
    {
        Task<List<MembersModel>> GetAllMembersAsync();
        Task<OperationResult<MembersModel>> AddMemberAsync(MembersModel membersModel);
        Task<OperationResult<LoginDTO>> LoginMemberAsync(string email, string password);
    }
}
