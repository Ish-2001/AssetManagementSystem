using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AssetManagementSystemUI.Services;

namespace AssetManagementSystemUI.Controllers
{
    public class HardwareController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HardwareService _hardwareService;

        public HardwareController(IHttpContextAccessor httpContextAccessor, HardwareService hardwareService)
        {
            _contextAccessor = httpContextAccessor;
            _hardwareService = hardwareService;
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await _hardwareService.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(HardwareDTO hardware)
        {
            var result = _hardwareService.Add(hardware);

            if (result.IsSuccessStatusCode)
            {
                TempData["addmessage"] = "Hardware has been added successfully";
                return RedirectToAction("GetAll");
            }

            TempData["addmessage"] = "Hardware with this model number is already present in the system";
            return View();
        }

        public IActionResult Delete(string modelNumber)
        { 
            try
            {
                _hardwareService.Delete(modelNumber);
                TempData["deletemessage"] = "The hardware has been deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["deletemessage"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Edit(HardwareDTO hardware)
        {
            return View(hardware);
        }

        [HttpPost]
        public IActionResult Edit(HardwareDTO hardware, string modelNumber)
        {
            var result = _hardwareService.Edit(hardware,modelNumber);

            if (result.IsSuccessStatusCode)
            {
                TempData["updatemessage"] = "The Hardware has been updated successfully";
                return RedirectToAction("GetAll");
            }

            TempData["updatemessage"] = "The Hardware cannot be updated";
            return View();
        }

        public IActionResult Availability()
        {
            List<HardwareDTO> hardwares = _hardwareService.Availability();
            return View(hardwares);
        }
    }
}
