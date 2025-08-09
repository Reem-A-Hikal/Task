using AutoMapper;
using EmployeeCustomProp.Models;
using EmployeeCustomProp.Repositories.Interfaces;
using EmployeeCustomProp.Services.Interfaces;
using EmployeeCustomProp.ViewModels.EmployeeVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeCustomProp.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPropertyDefinitionService _propertyDefinitionService;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper,
            IPropertyDefinitionService propertyDefinitionService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _propertyDefinitionService = propertyDefinitionService;
        }
        public async Task<IEnumerable<EmployeeViewModel>> GetAllEmployeesWithPropertiesAsync()
        {
            var employees = await _employeeRepository.GetAllWithPropertiesAsync();
            return _mapper.Map<List<EmployeeViewModel>>( employees );
        }

        public async Task<EmployeeViewModel?> GetByIdsWithPropertiesAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdWithPropertiesAsync(id);
            return _mapper.Map<EmployeeViewModel>(employee);
        }

        public async Task<EmployeeCreateViewModel> GetEmployeeCreateViewModel()
        {
            var vm =  new EmployeeCreateViewModel
            {
                CustomProperties = await BuildCustomPropertiesAsync()
            };
            return vm;
        }

        public async Task PopulateDropdownOptionsAsync(EmployeeCreateViewModel viewModel)
        {
            viewModel.CustomProperties = await BuildCustomPropertiesAsync(viewModel.CustomProperties);
        }

        public async Task CreateEmployeeAsync(EmployeeCreateViewModel viewModel)
        {
            var employee = new Employee
            {
                Code = viewModel.Code,
                Name = viewModel.Name,
                PropertyValues = viewModel.CustomProperties
                .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                .Select(p => new PropertyValue
                {
                    PropertyDefinitionId = p.PropertyDefinitionId,
                    Value = p.Value
                }).ToList()
            };
            await _employeeRepository.AddAsync(employee);
        }

        public async Task<EmployeeCreateViewModel> GetEmployeeForEditAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdWithPropertiesAsync(id);

            var properties = await _propertyDefinitionService.GetAllAsync();

            var customProps = properties.Select(prop =>
            {
                var existingValue = employee?.PropertyValues
                    .FirstOrDefault(pv => pv.PropertyDefinitionId == prop.Id)?.Value;
                return new EmployeeCustomPropertyVM
                {
                    PropertyDefinitionId = prop.Id,
                    Name = prop.Name,
                    Type = prop.Type.ToString(),
                    IsRequired = prop.IsRequired,
                    Value = existingValue,
                    DropdownOptions = prop.Type == Models.Enums.PropertyType.Dropdown
                        ? prop.DropdownOptions?.Select(o => new SelectListItem { Value = o, Text = o }).ToList()
                        : new List<SelectListItem>()
                };
            }).ToList();

            return new EmployeeCreateViewModel
            {
                Id = employee.Id,
                Code = employee.Code,
                Name = employee.Name,
                CustomProperties = customProps
            };
        }

        public async Task UpdateEmployeeAsync(EmployeeCreateViewModel viewModel)
        {
            var employee = await _employeeRepository.GetByIdWithPropertiesAsync(viewModel.Id);

            employee.Code = viewModel.Code;
            employee.Name = viewModel.Name;

            foreach (var prop in viewModel.CustomProperties)
            {
                var existingValue = employee.PropertyValues
                    .FirstOrDefault(pv => pv.PropertyDefinitionId == prop.PropertyDefinitionId);
                if (existingValue != null)
                {
                    existingValue.Value = prop.Value;
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(prop.Value))
                    {
                        employee.PropertyValues.Add(new PropertyValue
                        {
                            Value = prop.Value,
                            PropertyDefinitionId = prop.PropertyDefinitionId
                        });
                    }
                }
            }
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task SaveAsync()
        {
            await _employeeRepository.SaveChangesAsync();
        }

        private async Task<List<EmployeeCustomPropertyVM>> BuildCustomPropertiesAsync(
            IEnumerable<EmployeeCustomPropertyVM>? existingProperties = null)
        {
            var propDefinitions = await _propertyDefinitionService.GetAllAsync();

            var result = propDefinitions.Select(pd =>
            {
                var vm = existingProperties?.FirstOrDefault(ep => ep.PropertyDefinitionId == pd.Id)
                         ?? new EmployeeCustomPropertyVM { PropertyDefinitionId = pd.Id };

                vm.Name = pd.Name;
                vm.Type = pd.Type.ToString();
                vm.IsRequired = pd.IsRequired;
                vm.DropdownOptions = pd.Type == Models.Enums.PropertyType.Dropdown
                    ? pd?.DropdownOptions?.Select(o => new SelectListItem { Value = o, Text = o }).ToList()
                    : new List<SelectListItem>();

                return vm;
            }).ToList();

            return result;
        }
    }
}
