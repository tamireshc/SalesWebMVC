using Microsoft.EntityFrameworkCore;
using salesWebMvc.Data;
using salesWebMvc.Models;

namespace salesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly salesWebMvcContext _context;

        public DepartmentService(salesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAll()
        {
            return await _context.Departments.OrderBy(d=>d.Name).ToListAsync();
        }

    }
}
