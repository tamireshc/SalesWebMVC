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

        public List<Department> FindAll()
        {
            return _context.Departments.OrderBy(d=>d.Name).ToList();
        }

    }
}
