using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTwo.Infrastruture.Data;
using ProjectTwo.Entities.Models;
using ProjectTwo.Application.Interfaces;
using ProjectTwo.Application.Services;

namespace ProjectTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet]
        [Route("GetClients")]
        public async Task<ActionResult<List<ClientsModel>>> GetClients()
        {
            var clients = await _clientsService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpPost]
        [Route("AddClients")]
        public async Task<ActionResult<ClientsModel>> AddClients(ClientsModel clients)
        {
            var addClient = await _clientsService.AddClientsAsync(clients);

            return Ok(clients);
        }

        //[HttpPut]
        //[Route("UpdateAddress")]
        //public async Task<ActionResult<ClientsModel>> UpdateAddress(int id, string address)
        //{
        //    try
        //    {
        //        var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();
        //        if (idClient != null)
        //        {
        //            idClient.Address = address;
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            return NotFound("Clinte não localizado");
        //        }

        //        return Ok(idClient);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPut]
        //[Route("InactiveClient")]
        //public async Task<ActionResult<ClientsModel>> InactiveClient(int id)
        //{
        //    try
        //    {
        //        var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();
        //        if (idClient != null)
        //        {
        //            idClient.StateCode = false;
        //            idClient.IsDeleted = true;
        //            idClient.DeletedOn = DateTime.UtcNow;

        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            return NotFound("Cliente não encontrado.");
        //        }

        //        return Ok(idClient);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpDelete]
        //[Route("DeleteClient")]
        //public async Task<ActionResult<ClientsModel>> DeleteClient(int id)
        //{
        //    try
        //    {
        //        var idClient = _context.Clients.Where(c => c.Id == id).FirstOrDefault();
        //        if (idClient != null)
        //        {
        //            if (idClient.StateCode == true)
        //            {
        //                return Ok(new { warning = "Não é possível Deletar um Cliente ativo." });
        //            }
        //            else
        //            {
        //                _context.Clients.Remove(idClient);
        //                await _context.SaveChangesAsync();

        //                return Ok(new {message = $"o Cliente '{idClient.Name}', de Id '{idClient.Id}' foi deletado."});
        //            }
        //        }
        //        else
        //        {
        //            return NotFound("Cliente não encontrado.");
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
