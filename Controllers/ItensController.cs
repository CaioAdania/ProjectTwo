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

        [HttpGet]
        [Route("{id}GetItemById")]
        public async Task<ActionResult<ItensModel>> GetItemById(int id)
        {
            var getItem = await _itensService.GetItemByIdAsync(id);

            if(getItem.Success)
            {
                return Ok(getItem);
            }

            return BadRequest(new
            {
                Message = getItem.ErrorMessage,
                ErrorType = getItem.ErrorType
            });
        }

        //[HttpPost]
        //[Route("AddItem")]
        //public async Task<ActionResult<ItensModel>> AddItens(ItensModel itens)
        //{
        //    _context.Itens.Add(itens);
        //    await _context.SaveChangesAsync();

        //    return Ok(itens);
        //}

        //[HttpPut]
        //[Route("InactiveItem")]
        //public async Task<ActionResult<ItensModel>> InactiveItem(int id)
        //{
        //    try
        //    {
        //        var inactive = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

        //        if (inactive != null)
        //        {
        //            if (inactive.Amout != 0)
        //            {
        //                return Ok(new { warning = "Não é possível inativar produtos que possuem estoque." });
        //            }
        //            else
        //            {
        //                inactive.StateCode = false;
        //                inactive.IsDeleted = true;
        //                inactive.DeletedOn = DateTime.Now;

        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //        else
        //        {
        //            return NotFound("Produto não encontrado.");
        //        }

        //        return Ok(inactive);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
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
