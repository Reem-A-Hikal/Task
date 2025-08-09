using EmployeeCustomProp.Services.Interfaces;
using EmployeeCustomProp.ViewModels.PropertyValueVMs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCustomProp.Controllers
{
    public class PropertyValueController : Controller
    {
        private readonly IPropertyValueService _propertyValueService;


        public PropertyValueController(IPropertyValueService propertyValueService)
        {
            _propertyValueService = propertyValueService;
        }
        public async Task<IActionResult> Index()
        {
            var propertyValues = await _propertyValueService.GetAllAsync();
            return View(propertyValues);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _propertyValueService.BuildEditViewModelAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PropertyValueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var returnModel = await _propertyValueService.BuildEditViewModelAsync(model.Id);
                returnModel.Value = model.Value;
                return View(returnModel);
            }

            await _propertyValueService.Update(new()
            {
                Id = model.Id,
                Value = model.Value
            });
            await _propertyValueService.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}
