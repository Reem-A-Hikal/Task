using AutoMapper;
using EmployeeCustomProp.Models;
using EmployeeCustomProp.Models.Enums;
using EmployeeCustomProp.Repositories.Interfaces;
using EmployeeCustomProp.Services.Interfaces;
using EmployeeCustomProp.ViewModels.PropertyValueVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeCustomProp.Services.Implementation
{
    public class PropertyValueService : IPropertyValueService
    {
        private readonly IPropertyValueRepository _repo;
        private readonly IDropdownOptionService _dropdownOptionService;
        private readonly IMapper _mapper;

        public PropertyValueService(IPropertyValueRepository repo,
            IDropdownOptionService dropdownOptionService,
            IMapper mapper)
        {
            _repo = repo;
            _dropdownOptionService = dropdownOptionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyValueViewModel>> GetAllAsync()
        {
            var propValues = await _repo.GetAllAsync();
            return _mapper.Map<List<PropertyValueViewModel>>(propValues);
        }
        public async Task<PropertyValue> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task AddAsync(PropertyValue propertyValue) => await _repo.AddAsync(propertyValue);
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<bool> Update(PropertyValueEditVM propertyValue)
        {
            var propValue = await _repo.GetByIdAsync(propertyValue.Id);
            if (propValue == null) return false;

            propValue.Value = propertyValue.Value;
            return true;
        }

        public async Task<IEnumerable<PropertyValue>> GetByEmployeeIdAsync(int employeeId) => await _repo.GetByEmployeeIdAsync(employeeId);

        public async Task SaveAsync()
        {
            await _repo.SaveChangesAsync();
        }

        public async Task<List<SelectListItem>> LoadDropdownOptionsAsyncIfDropdownType(PropertiesDefinition prop)
        {
            if (prop.Type == PropertyType.Dropdown)
            {
                return await _dropdownOptionService.GetOptionsByPropertyIdAsync(prop.Id);
            }
            return new List<SelectListItem>();
        }

        public async Task<PropertyValueViewModel?> BuildEditViewModelAsync(int id)
        {
            var value = await _repo.GetByIdAsync(id);
            if (value == null) return null;

            var propDef = value.PropertyDefinition;

            return new PropertyValueViewModel
            {
                Id = id,
                Value = value.Value,
                EmployeeId = value.EmployeeId,
                EmployeeName = value.Employee.Name,
                PropertyId = propDef.Id,
                PropertyName = propDef.Name,
                PropertyType = propDef.Type.ToString(),
                Options = await LoadDropdownOptionsAsyncIfDropdownType(propDef)
            };
        }
    }
}
