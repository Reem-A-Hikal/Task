using EmployeeCustomProp.Models;
using EmployeeCustomProp.ViewModels.PropertyDefinitionVMs;

namespace EmployeeCustomProp.Services.Interfaces
{
    public interface IPropertyDefinitionService
    {
        Task<List<PropertyDefinitionViewModel>> GetAllAsync();
        Task<PropertiesDefinition> GetByIdAsync(int id);
        Task AddPropertyAsync(CreatePropertyDefinitionViewModel model);
        Task UpdatePropertyAsync(PropertiesDefinition property);
    }
}
