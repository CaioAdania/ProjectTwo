using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ProjectTwo.Entities.Models;
using ProjectTwo.Infrastruture.Data;

namespace ProjectTwo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItensController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ItensController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetItens")]
        public async Task<ActionResult<List<ItensModel>>> GetItens()
        {
            var itens = await _context.Itens.ToListAsync();

            return Ok(itens);
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<ActionResult<ItensModel>> AddItens(ItensModel itens)
        {
            _context.Itens.Add(itens);
            await _context.SaveChangesAsync();

            return Ok(itens);
        }

        [HttpPut]
        [Route("InactiveItem")]
        public async Task<ActionResult<ItensModel>> InactiveItem(int id)
        {
            try
            {
                var inactive = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

                if (inactive != null)
                {
                    if (inactive.Amout != 0)
                    {
                        return Ok(new { warning = "Não é possível inativar produtos que possuem estoque." });
                    }
                    else
                    {
                        inactive.StateCode = false;
                        inactive.IsDeleted = true;
                        inactive.DeletedOn = DateTime.Now;

                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    return NotFound("Produto não encontrado.");
                }

                return Ok(inactive);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("DeleteItem")]
        public async Task<ActionResult<ItensModel>> DeleteItem(int id)
        {
            try
            {
                var deleteItem = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

                if (deleteItem != null)
                {
                    if (deleteItem.StateCode == true)
                    {
                        return Ok(new { warning = "Não é possível deletar um item ativo no sistema" });
                    }
                    else
                    {
                        _context.Itens.Remove(deleteItem);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    return NotFound("Produto não encontrado.");
                }

                return Ok(new {message= $"O produto '{deleteItem.Name}', de Id '{deleteItem.Id}', foi deletado."});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateAmoutItens")]
        public async Task<ActionResult<ItensModel>> UpdateAmountItens(int id, int amount)
        {
            try
            {
                var item = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

                if (item != null)
                {
                    item.Amout = item.Amout - amount;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound("Produto não encontrado.");
                }

                return Ok(item);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
