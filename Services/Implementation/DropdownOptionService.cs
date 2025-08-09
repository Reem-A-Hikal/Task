using AutoMapper;
using EmployeeCustomProp.Repositories.Interfaces;
using EmployeeCustomProp.Services.Interfaces;
using EmployeeCustomProp.ViewModels.DropdownVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeCustomProp.Services.Implementation
{
    public class DropdownOptionService : IDropdownOptionService
    {
        private readonly IDropdownOptionRepository _dropdownOptionRepository;
        private readonly IMapper _mapper;

        public DropdownOptionService(IDropdownOptionRepository dropdownOptionRepository,
            IMapper mapper)
        {
            _dropdownOptionRepository = dropdownOptionRepository;
            _mapper = mapper;
        }
        public async Task<List<SelectListItem>> GetOptionsByPropertyIdAsync(int propertyId)
        {
            var options =  await _dropdownOptionRepository.GetOptionsByPropertyIdAsync(propertyId);
            return options.Select(x => new SelectListItem
            {
                Text = x.Text,
                Value = x.Value,
            }).ToList();
        }
    }
}
