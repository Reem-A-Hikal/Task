using EmployeeCustomProp.Models;
using EmployeeCustomProp.Models.DB;
using EmployeeCustomProp.Repositories.Interfaces;
using EmployeeCustomProp.ViewModels.DropdownVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCustomProp.Repositories.Implementation
{
    public class DropdownOptionRepository : GenericRepository<DropdownOption>, IDropdownOptionRepository
    {
        private readonly AppDbContext _context;
        public DropdownOptionRepository(AppDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<List<SelectListItem>> GetOptionsByPropertyIdAsync(int propertyId)
        {
            var options = await _context.DropdownOptions
                .Where(o => o.PropertyDefinitionId == propertyId)
                .Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Value
                })
                .ToListAsync();

            return options;
        }
    }
}
