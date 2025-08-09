using EmployeeCustomProp.Models;
using EmployeeCustomProp.Models.DB;
using EmployeeCustomProp.Repositories.Implementation;
using EmployeeCustomProp.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCustomProp.Repositories.Classes
{
    public class PropertyDefinitionRepository : GenericRepository<PropertiesDefinition>, IPropertyDefinitionRepository
    {
        private readonly AppDbContext _context;
        public PropertyDefinitionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<PropertiesDefinition> GetByIdAsync(int id)
        {
            return await _context.PropertyDefinitions
                .Include(p => p.DropdownOptions)
                .FirstOrDefaultAsync(p => p.Id == id);

        }

        public new async Task<IEnumerable<PropertiesDefinition>> GetAllAsync()
        {
            return await _context.PropertyDefinitions.Include(p => p.DropdownOptions).ToListAsync();
        }

    }
}
