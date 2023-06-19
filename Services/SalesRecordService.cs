using Microsoft.EntityFrameworkCore;
using salesWebMvc.Data;
using salesWebMvc.Models;

namespace salesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly salesWebMvcContext _context;

        public SalesRecordService(salesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDate(DateTime? mindate, DateTime? maxDate) 
        {
            var result = await _context.SalesRecords
                .Where(s => s.Date >= mindate && s.Date <= maxDate)
                .Include(s => s.Seller)
                .Include(s=>s.Seller.Department)
                .OrderBy(d => d.Date)
                .ToListAsync();
                  
            return result;
        }

        public async Task<List<IGrouping<string, SalesRecord>>> FindByDateGrouping(DateTime? mindate, DateTime? maxDate)
        {
            var result = await _context.SalesRecords
                .Where(s => s.Date >= mindate && s.Date <= maxDate)
                .Include(s => s.Seller)
                .OrderBy(d => d.Date)
                .GroupBy(D=>D.Seller.Department.Name)
                .ToListAsync();

            return result;
        }
    }
}
