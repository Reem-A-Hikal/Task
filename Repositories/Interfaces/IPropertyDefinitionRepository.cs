using EmployeeCustomProp.Models;

namespace EmployeeCustomProp.Repositories.Interfaces
{
    public interface IPropertyDefinitionRepository : IGenericRepository<PropertiesDefinition>
    {
        Task<PropertiesDefinition> GetByIdAsync(int id);
        Task<IEnumerable<PropertiesDefinition>> GetAllAsync();
    }
}
