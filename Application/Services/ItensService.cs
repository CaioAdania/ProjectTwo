using ProjectTwo.Application.Interfaces;
using ProjectTwo.Infrastruture.Data;
using ProjectTwo.Entities.Models;
using Microsoft.EntityFrameworkCore;
using ProjectTwo.Entities.Response;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProjectTwo.Application.Services
{
    public class ItensService : IItensService
    {
        private readonly AppDbContext _context;

        public ItensService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItensModel>> GetAllItensAsync()
        {
            return await _context.Itens.Where(i => !i.IsDeleted).ToListAsync();
        }

        public async Task<OperationResult<ItensModel>>GetItemByIdAsync(int id)
        {
            var result = new OperationResult<ItensModel>();
            var getItem = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

            if (getItem == null)
            {
                return result.Fail("Item não encontrado.", "NotFound 404");
            }

            return result.Ok(getItem);
        }

        public async Task<OperationResult<ItensModel>>AddItensAsync(ItensModel itens)
        {
            var result = new OperationResult<ItensModel>();

            try
            {
                _context.Itens.Add(itens);
                _context.SaveChanges();

                return result.Ok(itens);
            }
            catch
            {
                return result.Fail("Faltam campos a serem preenchidos.", "BadRequest 400");
            }
        }

        public async Task<OperationResult<ItensModel>>InactiveItemAsync(int id)
        {
            var result = new OperationResult<ItensModel>();
            var inactiveItem = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

            if (inactiveItem == null)
            {
                return result.Fail("Item não foi encontrado.", "NotFound 404");
            }
            if(inactiveItem.StateCode == false)
            {
                return result.Fail("Item já inativad.o", "BadRequest 400");
            }
            if(inactiveItem.Amout != 0)
            {
                return result.Fail("Não é possivel inativar um item em estoque.", "BadRequest 400");
            }

            inactiveItem.StateCode = false;
            _context.SaveChanges();

            return result.Ok(inactiveItem);
        }

        public async Task<OperationResult<ItensModel>>ActiveItemAsync(int id)
        {
            var result = new OperationResult<ItensModel>();
            var activeItem = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

            if(activeItem == null)
            {
                return result.Fail("Item não foi encontrado.", "NotFound 404");
            }
            if(activeItem.StateCode == true)
            {
                return result.Fail("Item já ativo.","BadRequest 400");
            }
            if(activeItem.IsDeleted == true)
            {
                return result.Fail("Item foi excluido, entre em contato com a equipe de Ti", "BadRequest 400");
            }

            activeItem.StateCode = true;
            _context.SaveChanges();

            return result.Ok(activeItem);
        }

        public async Task<OperationResult<ItensModel>> DeleteItemAsync(int id)
        {
            var result = new OperationResult<ItensModel>();
            var deleteItem = _context.Itens.Where(i => i.Id==id).FirstOrDefault();

            if(deleteItem == null)
            {
                return result.Fail("Item não foi encontrado.","NotFound 404");
            }
            if(deleteItem.StateCode == true)
            {
                return result.Fail("Não é possivel deletar um item ativo.","BadRequest 400");
            }

            deleteItem.IsDeleted = true;
            deleteItem.DeletedOn = DateTime.Now;

            _context.SaveChanges();

            return result.Ok(deleteItem);
           
        }

        public async Task<OperationResult<ItensModel>> RemoveItemAsync(int id, int amount)
        {
            var result = new OperationResult<ItensModel>();
            var idItem = _context.Itens.Where(i => i.Id == id).FirstOrDefault();

            if(idItem == null)
            {
                return result.Fail("Item não foi localizado.", "NotFound 404");
            }
            if(idItem.Amout == '0')
            {
                return result.Fail("Não existe estoque para este item.", "BadRequest 400");
            }
            if(amount == 0)
            {
                return result.Fail("Nenhum item foi subtraido", "BadRequest 400");
            }
            if(amount > idItem.Amout)
            {
                return result.Fail($"Não é possivel removar uma quantidade maior que a atual, sendo ela {idItem.Amout}", "BadRequest 400");
            }

            idItem.Amout = idItem.Amout - amount;

            _context.SaveChanges();

            return result.Ok(idItem);
        }
    }
}
