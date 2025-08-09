using EmployeeCustomProp.Services.Interfaces;
using EmployeeCustomProp.ViewModels.EmployeeVMs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCustomProp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesWithPropertiesAsync();
            return View(employees);
        }

        public async Task<ActionResult> Details(int id)
        {
            var employee = await _employeeService.GetByIdsWithPropertiesAsync(id);

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {   
            var vm = await _employeeService.GetEmployeeCreateViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                await _employeeService.PopulateDropdownOptionsAsync(ViewModel);
                return View(ViewModel);
            }
            await _employeeService.CreateEmployeeAsync(ViewModel);
            await _employeeService.SaveAsync();

            TempData["SuccessMessage"] = "Employee created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeForEditAsync(id);            
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await _employeeService.PopulateDropdownOptionsAsync(viewModel);
                return View(viewModel);
            }
            await _employeeService.UpdateEmployeeAsync(viewModel);

            TempData["SuccessMessage"] = "Employee updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
