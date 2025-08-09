using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.ViewModels.EmployeeVMs
{
    public class EmployeePropertyValueViewModel
    {
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Name cannot contain spaces")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string PropertyName { get; set; }
        public string? Value { get; set; }
    }
}
