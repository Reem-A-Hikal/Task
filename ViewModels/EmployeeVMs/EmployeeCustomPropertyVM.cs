using EmployeeCustomProp.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.ViewModels.EmployeeVMs
{
    public class EmployeeCustomPropertyVM
    {
        public int PropertyDefinitionId { get; set; }

        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Name cannot contain spaces")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }

        public string? Value { get; set; }

        public List<SelectListItem>? DropdownOptions { get; set; }
    }
}
