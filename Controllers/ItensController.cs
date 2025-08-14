using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ProjectTwo.Application.Services;
using ProjectTwo.Entities.Models;
using ProjectTwo.Infrastruture.Data;
using ProjectTwo.Application.Interfaces;
using System.Net.Sockets;

namespace ProjectTwo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItensController : ControllerBase
    {
        private readonly IItensService _itensService;
        public ItensController(IItensService itensService)
        {
            _itensService = itensService;
        }

        /// <summary>
        /// Traz todos os Itens.
        /// </summary>
        /// <returns>Retorna todos os itens da base de dados.</returns>
        [HttpGet]
        [Route("GetItens")]
        public async Task<ActionResult<List<ItensModel>>> GetItens()
        {
            try
            {
                var getItens = await _itensService.GetAllItensAsync();
                return Ok(getItens);
            }
            catch
            {
                return BadRequest("Erro no serviço");
            }
        }


        /// <summary>
        /// Traz um item especifico.
        /// </summary>
        /// <returns>Retorna item especifico da base de dados.</returns>
        [HttpGet]
        [Route("{id}GetItemById")]
        public async Task<ActionResult<ItensModel>> GetItemById(int id)
        {
            var getItem = await _itensService.GetItemByIdAsync(id);

            if (getItem.Success)
            {
                return Ok(getItem);
            }

            return BadRequest(new
            {
                Message = getItem.ErrorMessage,
                ErrorType = getItem.ErrorType
            });
        }


        /// <summary>
        /// Inclui um item a base de dados.
        /// </summary>
        /// <returns>Retorna o item incluido.</returns>
        [HttpPost]
        [Route("AddItem")]
        public async Task<ActionResult<ItensModel>> AddItens(ItensModel itens)
        {
            var addItem = await _itensService.AddItensAsync(itens);

            if (addItem.Success)
            {
                return Ok(addItem);
            }

            return BadRequest(new
            {
                Message = addItem.ErrorMessage,
                ErrorType = addItem.ErrorType
            });
        }

        /// <summary>
        /// Inativa um item.
        /// </summary>
        /// <returns>Retorna o item inativado.</returns>
        [HttpPut]
        [Route("{id}InactiveItem")]
        public async Task<ActionResult<ItensModel>> InactiveItem(int id)
        {
            try
            {
                var inactiveItem = await _itensService.InactiveItemAsync(id);

                if (inactiveItem.Success)
                {
                    return Ok($"Item de Id: {id} foi inativado");
                }

                return BadRequest(new
                {
                    Message = inactiveItem.ErrorMessage,
                    ErrorType = inactiveItem.ErrorType
                });
            }
            catch
            {
                return BadRequest("Erro no serviço.");
            }
        }

        /// <summary>
        /// Ativa um item.
        /// </summary>
        /// <returns>Retorna o item ativado.</returns>
        //[HttpPut]
        //[Route("{id}ActiveItem")]
        //public async Task<ActionResult<ItensModel>> ActiveItem(int id)
        //{
        //    try
        //    {
        //        var activeItem = await _itensService.ActiveItemAsync(id);

        //        if (activeItem.Success)
        //        {
        //            return Ok($"Item de Id: {id} foi ativado");
        //        }

        //        return BadRequest(new
        //        {
        //            Message = activeItem.ErrorMessage,
        //            ErrorType = activeItem.ErrorType
        //        });
        //    }
        //    catch
        //    {
        //        return BadRequest("Erro no serviço.");
        //    }
        //}

        //[HttpDelete]
        //[Route("DeleteItem")]
        //public async Task<ActionResult<ItensModel>> DeleteItem(int id)
        //{
        //    try
        //    {
        //        var deleteItem = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

        //        if (deleteItem != null)
        //        {
        //            if (deleteItem.StateCode == true)
        //            {
        //                return Ok(new { warning = "Não é possível deletar um item ativo no sistema" });
        //            }
        //            else
        //            {
        //                _context.Itens.Remove(deleteItem);
        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //        else
        //        {
        //            return NotFound("Produto não encontrado.");
        //        }

        //        return Ok(new {message= $"O produto '{deleteItem.Name}', de Id '{deleteItem.Id}', foi deletado."});
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPut]
        //[Route("UpdateAmoutItens")]
        //public async Task<ActionResult<ItensModel>> UpdateAmountItens(int id, int amount)
        //{
        //    try
        //    {
        //        var item = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

        //        if (item != null)
        //        {
        //            item.Amout = item.Amout - amount;
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            return NotFound("Produto não encontrado.");
        //        }

        //        return Ok(item);
        //    }
        //    catch(Exception ex)
        //    {
        ////        throw ex;
        //    }
        //}
    }
}
