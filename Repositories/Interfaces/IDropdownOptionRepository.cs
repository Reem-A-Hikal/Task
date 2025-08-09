using EmployeeCustomProp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeCustomProp.Repositories.Interfaces
{
    public interface IDropdownOptionRepository :IGenericRepository<DropdownOption>
    {
        Task<List<SelectListItem>> GetOptionsByPropertyIdAsync(int propertyId);
    }
}
