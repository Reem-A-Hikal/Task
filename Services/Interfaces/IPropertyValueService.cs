using EmployeeCustomProp.Models;
using EmployeeCustomProp.ViewModels.PropertyValueVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeCustomProp.Services.Interfaces
{
    public interface IPropertyValueService
    {
        Task<IEnumerable<PropertyValueViewModel>> GetAllAsync();
        Task<PropertyValue> GetByIdAsync(int id);
        Task AddAsync(PropertyValue propertyValue);
        Task<bool> Update(PropertyValueEditVM propertyValue);
        Task DeleteAsync(int id);
        Task<IEnumerable<PropertyValue>> GetByEmployeeIdAsync(int employeeId);
        Task<PropertyValueViewModel?> BuildEditViewModelAsync(int id);
        Task<List<SelectListItem>> LoadDropdownOptionsAsyncIfDropdownType(PropertiesDefinition prop);

        Task SaveAsync();
    }
}
