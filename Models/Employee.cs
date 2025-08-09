using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        [RegularExpression(@"^(?!\s)(?!.*\s$).*$", ErrorMessage = "Name cannot start or end with spaces")]
        public string Name { get; set; }


        // Navigation Property
        public ICollection<PropertyValue> PropertyValues { get; set; } = new List<PropertyValue>();
    }
}
