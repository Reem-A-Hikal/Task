namespace EmployeeCustomProp.ViewModels.DropdownVMs
{
    public class UpdateDropdownOptionViewModel
    {
        public int Id { get; set; }
        
        public string Value { get; set; }

        public int PropertyDefinitionId { get; set; }

        public string? PropertyDefinitionName { get; set; }
        
    }
}
