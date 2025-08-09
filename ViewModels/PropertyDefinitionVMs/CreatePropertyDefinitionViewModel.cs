using EmployeeCustomProp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.ViewModels.PropertyDefinitionVMs
{
    public class CreatePropertyDefinitionViewModel
    {
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Name cannot contain spaces")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? DropdownOptions { get; set; }
    }
}
