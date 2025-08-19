using ProjectTwo.Entities.Models;
using ProjectTwo.Entities.Response;

namespace ProjectTwo.Application.Interfaces
{
    public interface IMemberService
    {
        Task<List<MembersModel>> GetAllMembersAsync();
        Task<OperationResult<MembersModel>> AddMemberAsync(MembersModel membersModel);
        Task<OperationResult<MembersModel>> LoginMemberAsync(string email, string password);
    }
}
