using EmployeeCustomProp.Models;

namespace EmployeeCustomProp.Repositories.Interfaces
{
    public interface IPropertyValueRepository :IGenericRepository<PropertyValue>
    {
        new Task<IEnumerable<PropertyValue>> GetAllAsync();
        new Task<PropertyValue> GetByIdAsync(int id);
        Task<IEnumerable<PropertyValue>> GetByEmployeeIdAsync(int employeeId);
    }
}
