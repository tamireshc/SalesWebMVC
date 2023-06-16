using salesWebMvc.Data;
using salesWebMvc.Models;

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
            return _context.Sellers.FirstOrDefault(s => s.Id == id);
        }

        public void Remove(int id) 
        { 
            var seller = FindById(id);
            _context.Sellers.Remove(seller);
            _context.SaveChanges();
        }
    }
}
