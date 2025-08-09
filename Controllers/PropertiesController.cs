using AutoMapper;
using EmployeeCustomProp.Services.Interfaces;
using EmployeeCustomProp.ViewModels.PropertyDefinitionVMs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCustomProp.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyDefinitionService _propertyDefinitionService;
        private readonly IMapper _mapper;

        public PropertiesController(IMapper mapper, IPropertyDefinitionService propertyDefinitionService)
        {
            _propertyDefinitionService = propertyDefinitionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var definitions = await _propertyDefinitionService.GetAllAsync();
            return View(definitions);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var property = await _propertyDefinitionService.GetByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            var model =  _mapper.Map<PropertyDefinitionViewModel>(property);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreatePropertyDefinitionViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreatePropertyDefinitionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _propertyDefinitionService.AddPropertyAsync(model);

            TempData["SuccessMessage"] = "Property created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var property = await _propertyDefinitionService.GetByIdAsync(id);
            var propertyVM = _mapper.Map<PropertyDefinitionViewModel>(property);
            
            return View(propertyVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PropertyDefinitionViewModel Vmodel)
        {
            if(!ModelState.IsValid)
            {
                return View(Vmodel);
            }

            var existingProperty = await _propertyDefinitionService.GetByIdAsync(Vmodel.Id);
            _mapper.Map(Vmodel, existingProperty);

            await _propertyDefinitionService.UpdatePropertyAsync(existingProperty);

            TempData["SuccessMessage"] = "Property updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
