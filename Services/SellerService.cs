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

        public List<Seller> FindAll()
        {
            return _context.Sellers.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Sellers.Include(s=>s.Department).FirstOrDefault(s => s.Id == id);
        }

        public void Remove(int id) 
        { 
            var seller = FindById(id);
            _context.Sellers.Remove(seller);
            _context.SaveChanges();
        }

        public void Updade(Seller seller) { 
            if(!_context.Sellers.Any(s=>s.Id== seller.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try {
                _context.Sellers.Update(seller);
                _context.SaveChanges();
            }catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }     
        }
    }
}
