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
using ProjectTwo.Infrastruture.Auth;
using Microsoft.Extensions.Options;
using ProjectTwo.Application.DTOs;
using ProjectTwo.Infrastruture.Auth;

namespace ProjectTwo.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly JwtSettings _jwtSettings;
        public MemberService(AppDbContext context, IJwtTokenGenerator jwtTokenGenerator,IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _jwtSettings = jwtSettings.Value;
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
                var existingMember = await _context.Members.Where(m => m.Email == membersModel.Email).FirstOrDefaultAsync();
                if (existingMember != null)
                {
                    return result.Fail("E-mail já cadastrado.", "BadRequest 400");
                }

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

        public async Task<OperationResult<LoginDTO>> LoginMemberAsync(string email, string password)
        {
            var result = new OperationResult<LoginDTO>();
            try
            {
                var loginEmail = await _context.Members.Where(m => m.Email == email && m.StateCode != false).FirstOrDefaultAsync();

                if (loginEmail == null)
                {
                    return result.Fail("E-mail não encontrado.", "BadRequest 400");
                }

                bool loginPassword = BCrypt.Net.BCrypt.Verify(password, loginEmail.PasswordHash);
                if (!loginPassword)
                {
                    return result.Fail("Senha errada.","BadRequest 400");
                }

                var token = _jwtTokenGenerator.GenerateToken(loginEmail.Email, loginEmail.PasswordHash);

                var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

                var loginResponse = new LoginDTO
                {
                    Token = token,
                    Expiration = expiration,
                    User = new MemberDTO
                    {
                        Email = loginEmail.Email,
                        PasswordHash = loginEmail.PasswordHash
                    }
                };

                return result.Ok(loginResponse);
            }
            catch
            {
                return result.Fail("Usuario ou senha errado.", "BadRequest 400");
            }
            
        }
    }
}
