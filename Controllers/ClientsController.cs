using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTwo.Data;
using ProjectTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetClients")]
        public async Task<ActionResult<List<Clients>>> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();
            return Ok(clients);
        }

        [HttpPost]
        [Route("AddClients")]
        public async Task<ActionResult<Clients>> AddClients(Clients clients)
        {
            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return Ok(clients);
        }

        [HttpPut]
        [Route("UpdateAddress")]
        public async Task<ActionResult<Clients>> UpdateAddress(int id, string address)
        {
            try
            {
                var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();
                if (idClient != null)
                {
                    idClient.Address = address;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound("Clinte não localizado");
                }

                return Ok(idClient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("InactiveClient")]
        public async Task<ActionResult<Clients>> InactiveClient(int id)
        {
            try
            {
                var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();
                if (idClient != null)
                {
                    idClient.StateCode = false;
                    idClient.IsDeleted = true;
                    idClient.DeletedOn = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound("Cliente não encontrado.");
                }

                return Ok(idClient);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("DeleteClient")]
        public async Task<ActionResult<Clients>> DeleteClient(int id)
        {
            try
            {
                var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();
                if (idClient != null)
                {
                    if (idClient.StateCode == true)
                    {
                        return Ok(new { warning = "Não é possível Deletar um Cliente ativo." });
                    }
                    else
                    {
                        _context.Clients.Remove(idClient);
                        await _context.SaveChangesAsync();

                        return Ok(new {message = $"o Cliente '{idClient.Name}', de Id '{idClient.Id}' foi deletado."});
                    }
                }
                else
                {
                    return NotFound("Cliente não encontrado.");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
