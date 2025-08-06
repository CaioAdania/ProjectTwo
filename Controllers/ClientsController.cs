using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTwo.Infrastruture.Data;
using ProjectTwo.Entities.Models;
using ProjectTwo.Application.Interfaces;
using ProjectTwo.Application.Services;
using ProjectTwo.Application.DTOs;

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

        /// <summary>
        /// Traz todos os clientes da base de dados.
        /// </summary>
        /// <returns>Retorna todos os clientes da base de dados.</returns>
        [HttpGet]
        [Route("GetClients")]
        public async Task<ActionResult<List<ClientsModel>>> GetClients()
        {
            var clients = await _clientsService.GetAllClientsAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Adiciona um cliente a base da dados.
        /// </summary>
        /// <returns>Retorna o cliente incluido na base de dados.</returns>
        [HttpPost]
        [Route("AddClients")]
        public async Task<ActionResult<ClientsModel>> AddClients(ClientsModel clients)
        {
            var addClient = await _clientsService.AddClientsAsync(clients);

            return Ok(clients);
        }

        /// <summary>
        /// Atualiza o endereço de um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser atualizado.</param>
        /// <param name="address">Novo endereço do cliente.</param>
        /// <returns>Retorna o cliente atualizado.</returns>
        [HttpPut]
        [Route("{id}/UpdateAddress")]
        public async Task<ActionResult<ClientsModel>> UpdateAddress(int id, string address)
        {
            try
            {
                var idClient = await _clientsService.UpdateAddressUserAsync(id, address);

                var result = new ClientsAddressDTO
                {
                    Id = idClient.Id,
                    Address = idClient.Address
                };

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

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
