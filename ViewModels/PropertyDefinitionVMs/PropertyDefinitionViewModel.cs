using EmployeeCustomProp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.ViewModels.PropertyDefinitionVMs
{
    public class PropertyDefinitionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Name cannot contain spaces")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? DropdownOptions { get; set; } = new();
    }
}
