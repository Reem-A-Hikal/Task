using AutoMapper;
using EmployeeCustomProp.Models;
using EmployeeCustomProp.Models.DB;
using EmployeeCustomProp.ViewModels.DropdownVMs;
using EmployeeCustomProp.ViewModels.EmployeeVMs;
using EmployeeCustomProp.ViewModels.PropertyDefinitionVMs;
using EmployeeCustomProp.ViewModels.PropertyValueVMs;

namespace EmployeeCustomProp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PropertiesDefinition, PropertyDefinitionViewModel>()
                .ForMember(dest => dest.DropdownOptions,
                            opt => opt.MapFrom(src => src.DropdownOptions.Select(o => o.Value))).ReverseMap();

            CreateMap<CreatePropertyDefinitionViewModel, PropertiesDefinition>();

            CreateMap<DropdownOptionViewModel, DropdownOption>().ReverseMap();

            CreateMap<PropertyValue, PropertyValueViewModel>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.PropertyDefinition.Name))
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.PropertyDefinition.Type))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));

            CreateMap<PropertyValueViewModel, PropertyValue>();

            CreateMap<Employee, EmployeeViewModel>()
            .ForMember(dest => dest.CustomProperties,
                opt => opt.MapFrom((src, dest) =>
                {
                    return src.PropertyValues
                    .Select(pv => new EmployeePropertyValueViewModel
                    {
                        PropertyName = pv.PropertyDefinition.Name,
                        Value = pv.Value
                    }).ToList();
                })
            );

            //CreateMap<EmployeeViewModel, Employee>()
            //.ForMember(dest => dest.PropertyValues,
            //    opt => opt.MapFrom(src =>
            //        src.CustomProperties != null
            //        ? src.CustomProperties.Select(cp => new PropertyValue
            //        {
            //            Value = cp.Value
            //        }).ToList()
            //        : new List<PropertyValue>()
            //    )
            //);
        }
    }
}
