using EmployeeCustomProp.Models;
using EmployeeCustomProp.Models.DB;
using EmployeeCustomProp.Repositories.Implementation;
using EmployeeCustomProp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCustomProp.Repositories.Classes
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) :base(context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Employee>> GetAllWithPropertiesAsync()
        {
            return await _context.Employees
                .Include(e => e.PropertyValues)
                    .ThenInclude(pv => pv.PropertyDefinition)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdWithPropertiesAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.PropertyValues)
                    .ThenInclude(pv => pv.PropertyDefinition)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
