using EmployeeCustomProp.Models.DB;
using EmployeeCustomProp.Profiles;
using EmployeeCustomProp.Repositories.Classes;
using EmployeeCustomProp.Repositories.Implementation;
using EmployeeCustomProp.Repositories.Interfaces;
using EmployeeCustomProp.Services.Implementation;
using EmployeeCustomProp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCustomProp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IPropertyDefinitionRepository, PropertyDefinitionRepository>();
            builder.Services.AddScoped<IPropertyValueRepository, PropertyValueRepository>();
            builder.Services.AddScoped<IDropdownOptionRepository, DropdownOptionRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IPropertyDefinitionService, PropertyDefinitionService>();
            builder.Services.AddScoped<IPropertyValueService, PropertyValueService>();
            builder.Services.AddScoped<IDropdownOptionService, DropdownOptionService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
