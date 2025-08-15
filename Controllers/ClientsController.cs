using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTwo.Infrastruture.Data;
using ProjectTwo.Entities.Models;
using ProjectTwo.Application.Interfaces;
using ProjectTwo.Application.Services;
using ProjectTwo.Application.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace ProjectTwo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        /// <summary>
        /// Traz todos os clientes.
        /// </summary>
        /// <returns>Retorna todos os clientes da base de dados.</returns>
        [HttpGet]
        [Route("GetClients")]
        public async Task<ActionResult<List<ClientsModel>>> GetClients()
        {
            var getClients = await _clientsService.GetAllClientsAsync();
            return Ok(getClients);
        }

        /// <summary>
        /// Retorna o cliente com base no Id.
        /// </summary>
        /// <returns>Retorna o cliente pelo Id.</returns>
        [HttpGet]
        [Route("{id}/GetClientById")]
        public async Task<ActionResult<List<ClientsModel>>> GetClientById(int id)
        {
            try
            {
                var getClientId = await _clientsService.GetClientByIdAsync(id);

                if(getClientId.Success)
                {
                    return Ok(getClientId.Data);
                }

                return BadRequest(new
                {
                    Message = getClientId.ErrorMessage,
                    ErrorType = getClientId.ErrorType
                });

            }
            catch
            {
                return BadRequest("Erro no serviço.");
            }
        }

        /// <summary>
        /// Adiciona um cliente a base da dados.
        /// </summary>
        /// <returns>Retorna o cliente incluido na base de dados.</returns>
        [HttpPost]
        [Route("AddClients")]
        public async Task<ActionResult<ClientsModel>> AddClients(ClientsModel clients)
        {
            try
            {
                var addClient = await _clientsService.AddClientsAsync(clients);

                if(addClient.Success)
                {
                    return Ok(addClient.Data);
                }

                return BadRequest(new
                {
                    Message = addClient.ErrorMessage,
                    ErrorType = addClient.ErrorType
                });

            }
            catch
            {
                return BadRequest("Erro no servidor.");
            }
        }

        /// <summary>
        /// Atualiza um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser atualizado.</param>
        /// <returns>Retorna o cliente atualizado.</returns>
        [HttpPut]
        [Route("{id}/UpdateClient")]
        public async Task<ActionResult<ClientsModel>> UpdateClient(int id, [FromBody] ClientsDTO dto) //corrigir, está com erro
        {
            try
            {
                var idClient = await _clientsService.UpdateClientAsync(id, dto);

                if (idClient.Success)
                {
                    var result = new ClientsDTO
                    {
                        Id = dto.Id,
                        PhoneNumber = !string.IsNullOrEmpty(dto.PhoneNumber) ? dto.PhoneNumber : null,
                        Email = !string.IsNullOrEmpty(dto.Email) ? dto.Email : null,
                        Address = dto.Address,
                        City = dto.City,
                        Number = dto.Number > 0 ? dto.Number : (int?)null
                    };

                    return Ok(result);
                }

                return BadRequest(new
                {
                    Message = idClient.ErrorMessage,
                    ErrorType = idClient.ErrorType
                });

            }
            catch
            {
                return BadRequest("erro no servidor.");
            }
        }

        /// <summary>
        /// Inativa o cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente para ser inativado.</param>
        /// <returns> Retorna o Id do cliente inativado.</returns>
        [HttpPut]
        [Route("{id}/InactiveClient")]
        public async Task<ActionResult<ClientsModel>> InactiveClient(int id)
        {
            try
            {
                var idClient = await _clientsService.InactiveClientAsync(id);
                
                if (idClient.Success)
                {
                    return Ok($"O cliente de Id: {id}, foi inativado com sucesso.");
                }

                return BadRequest(new
                {
                    Message = idClient.ErrorMessage,
                    ErrorType = idClient.ErrorType
                });
                
            }
            catch
            {
                return BadRequest("Erro no serviço.");
            }
        }

        /// <summary>
        /// Ativa o cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente para ser ativado.</param>
        /// <returns> Retorna o Id do cliente ativado. </returns>
        [HttpPut]
        [Route("{id}/ActiveClient")]
        public async Task<ActionResult<ClientsModel>> ActiveClient(int id)
        {
            try
            {
                var idClient = await _clientsService.ActiveClientAsync(id);

                if(idClient.Success)
                {
                    return Ok($"O cliente de Id: {id}, foi ativado com sucesso.");
                }

                return BadRequest(new
                {
                    Message = idClient.ErrorMessage,
                    ErrorType = idClient.ErrorType
                });
                
            }
            catch
            {
                return BadRequest("Erro no serviço");
            }
        }

        /// <summary>
        /// Deleta o cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente para ser deletado.</param>
        /// <returns> Retorna o Id do cliente deletado. </returns>
        [HttpDelete]
        [Route("{id}/DeleteClient")]
        public async Task<ActionResult<ClientsModel>> DeleteClient(int id)
        {
            try
            {
                var idClient = await _clientsService.DeleteClientAsync(id);

                if (idClient.Success)
                {
                    return Ok($"O cliente de Id: {id}, foi deletado com sucesso.");
                }

                return BadRequest(new
                {
                    Message = idClient.ErrorMessage,
                    ErrorType = idClient.ErrorType
                });
            }
            catch
            {
                return BadRequest("Erro no serviço.");
            }
        }
    }
}
