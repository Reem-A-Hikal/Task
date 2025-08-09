using EmployeeCustomProp.ViewModels.DropdownVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeCustomProp.Services.Interfaces
{
    public interface IDropdownOptionService
    {
        Task<List<SelectListItem>> GetOptionsByPropertyIdAsync(int propertyId);
    }
}
