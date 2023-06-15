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
    }
}
