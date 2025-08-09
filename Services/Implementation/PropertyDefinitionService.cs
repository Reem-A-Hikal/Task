using AutoMapper;
using EmployeeCustomProp.Models;
using EmployeeCustomProp.Models.Enums;
using EmployeeCustomProp.Repositories.Interfaces;
using EmployeeCustomProp.Services.Interfaces;
using EmployeeCustomProp.ViewModels.PropertyDefinitionVMs;
using System.Reflection.Metadata;

namespace EmployeeCustomProp.Services.Implementation
{
    public class PropertyDefinitionService : IPropertyDefinitionService
    {
        private readonly IPropertyDefinitionRepository _propertyDefinitionRepository;
        private readonly IGenericRepository<DropdownOption> _dropdownOptionRepository;
        private readonly IMapper _mapper;
        public PropertyDefinitionService(
            IMapper mapper,
            IPropertyDefinitionRepository propertyDefinitionRepository,
            IGenericRepository<DropdownOption> dropdownOptionRepository)
        {
            _mapper = mapper;
            _propertyDefinitionRepository = propertyDefinitionRepository;
            _dropdownOptionRepository = dropdownOptionRepository;
        }


        public async Task<List<PropertyDefinitionViewModel>> GetAllAsync()
        {
            var definitions = await _propertyDefinitionRepository.GetAllAsync();
            return _mapper.Map<List<PropertyDefinitionViewModel>>(definitions);
        }

        public async Task<PropertiesDefinition> GetByIdAsync(int id)
        {
            return await _propertyDefinitionRepository.GetByIdAsync(id);
        }

        public async Task AddPropertyAsync(CreatePropertyDefinitionViewModel model)
        {
            var property = _mapper.Map<PropertiesDefinition>(model);

            await _propertyDefinitionRepository.AddAsync(property);
            await _propertyDefinitionRepository.SaveChangesAsync();

            if (property.Type == PropertyType.Dropdown && model.DropdownOptions != null)
            {
                foreach (var option in model.DropdownOptions)
                {
                    var dropdownOption = new DropdownOption
                    {
                        PropertyDefinitionId = property.Id,
                        Value = option
                    };
                    await _dropdownOptionRepository.AddAsync(dropdownOption);
                }
                await _dropdownOptionRepository.SaveChangesAsync();
            }
        }

        public async Task UpdatePropertyAsync(PropertiesDefinition property)
        {
             _propertyDefinitionRepository.Update(property);
            await _propertyDefinitionRepository.SaveChangesAsync();
        }
    }
}
