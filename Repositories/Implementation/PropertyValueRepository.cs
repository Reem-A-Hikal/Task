using EmployeeCustomProp.Models;
using EmployeeCustomProp.Models.DB;
using EmployeeCustomProp.Repositories.Implementation;
using EmployeeCustomProp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCustomProp.Repositories.Classes
{
    public class PropertyValueRepository : GenericRepository<PropertyValue>, IPropertyValueRepository
    {
        private readonly AppDbContext _context;

        public PropertyValueRepository(AppDbContext context) :base(context)
        {
            _context = context;
        }

        public async override Task<IEnumerable<PropertyValue>> GetAllAsync()
        {
            return await _context.PropertyValues
                .Include(pv => pv.Employee)
                .Include(pv => pv.PropertyDefinition)
                .ToListAsync();
        }
        public async override Task<PropertyValue> GetByIdAsync(int id)
        {
            return await _context.PropertyValues
                .Include(pv => pv.Employee)
                .Include(pv => pv.PropertyDefinition)
                .FirstOrDefaultAsync(pv => pv.Id == id);
        }

        public async Task<IEnumerable<PropertyValue>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.PropertyValues
                        .Where(p => p.EmployeeId == employeeId)
                        .Include(p => p.PropertyDefinition)
                        .ToListAsync();
        }
    }
}
