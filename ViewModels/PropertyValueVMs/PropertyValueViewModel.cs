using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.ViewModels.PropertyValueVMs
{
    public class PropertyValueViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }

        public int PropertyId { get; set; }

        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Name cannot contain spaces")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string? PropertyName { get; set; }
        public string? PropertyType { get; set; }

        public List<SelectListItem>? Options { get; set; }
    }
}
