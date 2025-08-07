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
        /// Retorna o cliente com base no Id da base de dados.
        /// </summary>
        /// <returns>Retorna o cliente pelo Id.</returns>
        [HttpGet]
        [Route("{id}/GetClientById")]
        public async Task<ActionResult<List<ClientsModel>>> GetClientById(int id)
        {
            var client = await _clientsService.GetClientByIdAsync(id);
            return Ok(client);
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
        /// <returns>Retorna o cliente atualizado.</returns>
        [HttpPut]
        [Route("{id}/UpdateClient")]
        public async Task<ActionResult<ClientsModel>> UpdateClient(int id, [FromBody] ClientsDTO dto)
        {
            try
            {
                var idClient = await _clientsService.UpdateClientAsync(id, dto);

                var result = new ClientsDTO
                {
                    Id = idClient.Id,
                    PhoneNumber = !string.IsNullOrEmpty(idClient.PhoneNumber) ? idClient.PhoneNumber : null,
                    Email = !string.IsNullOrEmpty(idClient.Email) ? idClient.Email : null,
                    Address = idClient.Address,
                    City = idClient.City,
                    Number = idClient.Number > 0 ? idClient.Number : (int?)null
                };

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Inativa o cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente para ser inativado.</param>
        /// <returns> Retorna o Id do cliente. </returns>
        [HttpPut]
        [Route("{id}/InactiveClient")]
        public async Task<ActionResult<ClientsModel>> InactiveClient(int id)
        {
            try
            {
                var idClient = _clientsService.InactiveClientAsync(id);

                return Ok($"O cliente de Id: {id}, foi inativado com sucesso.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Ativa o cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente para ser ativado.</param>
        /// <returns> Retorna o Id do cliente. </returns>
        [HttpPut]
        [Route("{id}/ActiveClient")]
        public async Task<ActionResult<ClientsModel>> ActiveClient(int id)
        {
            try
            {
                var idClient = _clientsService.ActiveClientAsync(id);

                return Ok($"O cliente de Id: {id}, foi ativado com sucesso.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

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
