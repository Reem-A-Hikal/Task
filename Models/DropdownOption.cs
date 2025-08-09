namespace EmployeeCustomProp.Models
{
    public class DropdownOption
    {
        public int Id { get; set; }
        public string Value { get; set; }

        // Foreign Key
        public int PropertyDefinitionId { get; set; }

        // Navigation Property
        public PropertiesDefinition PropertiesDefinition { get; set; }
    }
}
