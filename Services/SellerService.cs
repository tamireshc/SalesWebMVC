using Microsoft.EntityFrameworkCore;
using salesWebMvc.Data;
using salesWebMvc.Models;
using salesWebMvc.Services.Exception;

namespace salesWebMvc.Services
{
    public class SellerService
    {
        private readonly salesWebMvcContext _context;

        public SellerService(salesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAll()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task Insert(Seller seller)
        {
             _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindById(int id)
        {
            return await _context.Sellers.Include(s=>s.Department).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Remove(int id)
        {
            try
            {
                var seller = await FindById(id);
                _context.Sellers.Remove(seller);
                await _context.SaveChangesAsync();

            }catch(DbUpdateException e) {
                throw new IntegrityException(e.Message);
            }

        }

        public async Task Updade(Seller seller) { 
            bool hasAny = await _context.Sellers.AnyAsync(s => s.Id == seller.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try {
                _context.Sellers.Update(seller);
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }     
        }
    }
}
