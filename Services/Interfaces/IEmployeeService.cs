using EmployeeCustomProp.ViewModels.EmployeeVMs;

namespace EmployeeCustomProp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesWithPropertiesAsync();
        Task<EmployeeViewModel?> GetByIdsWithPropertiesAsync(int id);
        Task CreateEmployeeAsync(EmployeeCreateViewModel viewModel);
        Task<EmployeeCreateViewModel> GetEmployeeCreateViewModel();
        Task<EmployeeCreateViewModel> GetEmployeeForEditAsync(int id);
        Task UpdateEmployeeAsync(EmployeeCreateViewModel viewModel);
        Task PopulateDropdownOptionsAsync(EmployeeCreateViewModel viewModel);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
