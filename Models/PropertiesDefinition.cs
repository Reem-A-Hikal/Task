using EmployeeCustomProp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.Models
{
    public class PropertiesDefinition
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Name cannot contain spaces")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public PropertyType Type { get; set; }

        public bool IsRequired { get; set; }

        public ICollection<DropdownOption> DropdownOptions { get; set; }
    }
}
