using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services;
using AssetManagementSystemUI.Services.SoftwareLicenseService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagementSystemUI.Controllers
{
    public class SoftwareLicenseController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SoftwareLicenseService _softwareLicenseService;

        public SoftwareLicenseController(IHttpContextAccessor httpContextAccessor, SoftwareLicenseService softwareLicenseService)
        {
            _contextAccessor = httpContextAccessor;
            _softwareLicenseService = softwareLicenseService;
        }

        public async Task<IActionResult> GetAll()
        {
            return View( await _softwareLicenseService.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SoftwareLicenseDTO license)
        {
            var result = _softwareLicenseService.Add(license);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "license has been added successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "License with this license number is already present in the system";
            return View();
        }

        public IActionResult Delete(string licenseNumber)
        {
            try
            {                
                _softwareLicenseService.Delete(licenseNumber);
                TempData["message"] = "The license has been deleted successfully";
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Edit(SoftwareLicenseDTO license)
        {
            return View(license);
        }

        [HttpPost]
        public IActionResult Edit(SoftwareLicenseDTO license, string licenseNumber)
        {
            var result = _softwareLicenseService.Edit(license,licenseNumber);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The license has been updated successfully";
                return RedirectToAction("GetAll");
            }

            TempData["message"] = "The license cannot be updated";
            return View();
        }

        public IActionResult Availability()
        {
            List<SoftwareLicenseDTO> softwares = _softwareLicenseService.Availability();
            return View(softwares);
        }
    }
}
