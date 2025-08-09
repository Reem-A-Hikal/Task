using System.ComponentModel.DataAnnotations;

namespace EmployeeCustomProp.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public string? Value { get; set; }

        // Foreign Keys
        public int EmployeeId { get; set; }
        public int PropertyDefinitionId { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; }
        public PropertiesDefinition PropertyDefinition { get; set; }

    }
}
