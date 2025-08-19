using ProjectTwo.Infrastruture.Data;
using ProjectTwo.Application.Interfaces;
using ProjectTwo.Entities.Response;
using ProjectTwo.Controllers;
using ProjectTwo.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using BCrypt.Net;

namespace ProjectTwo.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _context;
        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MembersModel>> GetAllMembersAsync()
        {
            return await _context.Members.Where(m => m.StateCode != false).ToListAsync();
        }

        public async Task<OperationResult<MembersModel>> AddMemberAsync(MembersModel membersModel)
        {
            var result = new OperationResult<MembersModel>();

            try
            {
                var password = membersModel.PasswordHash;
                var ecryptPassword = BCrypt.Net.BCrypt.HashPassword(password);
                membersModel.PasswordHash = ecryptPassword;

                _context.Members.Add(membersModel);
                _context.SaveChanges();

                return result.Ok(membersModel);
            }
            catch
            {
                return result.Fail("Faltam campos a serem preenchidos.", "BadRequest 400");
            }
        }

        public async Task<OperationResult<MembersModel>> LoginMemberAsync(string email, string password)
        {
            var result = new OperationResult<MembersModel>();
            try
            {
                var loginEmail = await _context.Members.Where(m => m.Email == email).FirstOrDefaultAsync();

                if (loginEmail == null)
                {
                    return result.Fail("É necessario colocar o E-mail.", "BadRequest 400");
                }

                bool loginPassword = BCrypt.Net.BCrypt.Verify(password, loginEmail.PasswordHash);
                if (!loginPassword)
                {
                    return result.Fail("Senha errada.","BadRequest 400");
                }

                return result.Ok(loginEmail);
            }
            catch
            {
                return result.Fail("Usuario ou senha errado.", "BadRequest 400");
            }
            
        }
    }
}
