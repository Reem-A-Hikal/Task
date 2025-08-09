using EmployeeCustomProp.Models;

namespace EmployeeCustomProp.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllWithPropertiesAsync();
        Task<Employee?> GetByIdWithPropertiesAsync(int id);
    }
}
