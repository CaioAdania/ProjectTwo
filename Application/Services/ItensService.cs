using ProjectTwo.Application.Interfaces;
using ProjectTwo.Infrastruture.Data;
using ProjectTwo.Entities.Models;
using Microsoft.EntityFrameworkCore;
using ProjectTwo.Entities.Response;

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
                return result.Fail("Item não encontrado", "NotFound 404");
            }

            return result.Ok(getItem);
        }
    }
}
