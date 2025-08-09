using Microsoft.EntityFrameworkCore;

namespace EmployeeCustomProp.Models.DB
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PropertyValue> PropertyValues { get; set; }
        public virtual DbSet<PropertiesDefinition> PropertyDefinitions { get; set; }
        public virtual DbSet<DropdownOption> DropdownOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PropertiesDefinition>()
                .Property(p => p.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (Enums.PropertyType)Enum.Parse(typeof(Enums.PropertyType), v)
                );

            modelBuilder.Entity<DropdownOption>()
                .HasOne(d => d.PropertiesDefinition)
                .WithMany(p => p.DropdownOptions)
                .HasForeignKey(d => d.PropertyDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PropertyValue>()
                .HasOne(pv => pv.Employee)
                .WithMany(e => e.PropertyValues)
                .HasForeignKey(pv => pv.EmployeeId);

            modelBuilder.Entity<PropertyValue>()
                .HasOne(pv => pv.PropertyDefinition)
                .WithMany()
                .HasForeignKey(pv => pv.PropertyDefinitionId);

            modelBuilder.Entity<PropertiesDefinition>()
                .HasData(
                    new PropertiesDefinition { Id = 1, Name = "Age", Type = Enums.PropertyType.Integer, IsRequired = true },
                    new PropertiesDefinition { Id = 2, Name = "Department", Type = Enums.PropertyType.Dropdown, IsRequired = true }
                );

            modelBuilder.Entity<DropdownOption>()
                .HasData(
                    new DropdownOption { Id = 1, PropertyDefinitionId = 2, Value = "HR" },
                    new DropdownOption { Id = 2, PropertyDefinitionId = 2, Value = "IT" },
                    new DropdownOption { Id = 3, PropertyDefinitionId = 2, Value = "Finance" }
                );
        }
    }
}
