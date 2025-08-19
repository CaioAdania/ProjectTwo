using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTwo.Application.Interfaces;
using ProjectTwo.Entities.Models;
using System.Globalization;

namespace ProjectTwo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        [Route("GetAllMembers")]
        public async Task<ActionResult<List<MembersModel>>> GetAllMembers()
        {
            try
            {
                var getMembers = await _memberService.GetAllMembersAsync();
                return Ok(getMembers);
            }
            catch
            {
                return BadRequest("Erro no serviço.");
            }
        }

        [HttpPost]
        [Route("AddMembers")]
        public async Task<ActionResult<MembersModel>>AddMember(MembersModel membersModel)
        {
            try
            {
                var addMember = await _memberService.AddMemberAsync(membersModel);

                if (addMember.Success)
                {
                    return Ok(addMember.Data);
                }

                return BadRequest(new
                {
                    Message = addMember.ErrorMessage,
                    ErrorType = addMember.ErrorType
                });
            }
            catch
            {
                return BadRequest("Erro no serviço.");
            } 
        }

        [HttpPost]
        [Route("{email}/{password}/LoginMember")]
        public async Task<ActionResult<MembersModel>> LoginMember(string email, string password)
        {
            try
            {
                var loginUser = await _memberService.LoginMemberAsync(email, password);

                if(loginUser.Success)
                {
                    return Ok("Login realizado com sucesso.");
                }

                return BadRequest(new
                {
                    Message = loginUser.ErrorMessage,
                    ErrorType = loginUser.ErrorType
                });
            }
            catch
            {
                return BadRequest("Erro no serviço.");
            }
        }
    }
}
