using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services.Admin;
using AssetManagementSystemUI.Services.Asset;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;

namespace AssetManagementSystemUI.Controllers
{
    public class AdminController : Controller
    {
       
        private readonly AdminService _adminService;
        private readonly AssetService _assetService;
        private readonly IHttpContextAccessor _contextAccessor;
        public const string sessionToken = "admintoken";

        public AdminController(IHttpContextAccessor httpContextAccessor, AdminService adminService, AssetService assetService)
        {
            _adminService = adminService;
            _contextAccessor = httpContextAccessor;
            _assetService = assetService;
        }
        public IActionResult Index()
        {
            try
            {
                List<DataPointDTO> adminDataPoints = new List<DataPointDTO>();
                var assets = _adminService.GetAssignedAssets().Result
                             .Where(item => item.Status == Status.Assigned.ToString() || item.Status == Status.Accepted.ToString())
                             .GroupBy(item => item.AssetType);

                foreach (var item in assets)
                {
                    adminDataPoints.Add(new DataPointDTO(item.Key, item.Count()));
                }

                ViewBag.AdminDataPoints = JsonConvert.SerializeObject(adminDataPoints);
            }
			catch
            {
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Remove("admintoken");
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminLoginDTO login)
        {
            var result = _adminService.Login(login);

            if (result.IsSuccessStatusCode)
            {
                var token = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString(sessionToken, token);
                return RedirectToAction("Index");
            }
            TempData["message"] = "Invalid credentials.Please enter the credentials again";
            return View();
        }

        [HttpGet]
        public IActionResult Assign()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Assign(AssetDTO asset)
        {
            var result = _adminService.Assign(asset);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The asset is assigned successfully";
                return RedirectToAction("GetAssignedAssets");
            }
            TempData["message"] = "The asset cannot be assigned";
            return View();
        }

        public IActionResult Unassign(AssetDTO asset)
        {
            var result = _adminService.Unassign(asset);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The asset is unassigned successfully";
                return RedirectToAction("GetAssignedAssets");
            }
            TempData["message"] = "The asset cannot be unassigned";
            return RedirectToAction("GetAssignedAssets");
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignedAssets()
        {
            var result = await _adminService.GetAssignedAssets();
            return View(await _adminService.GetAssignedAssets());
        }

        [HttpGet]
        public async Task<IActionResult> GetRequestedAssets()
        {
            return View(await _adminService.GetRequestedAssets());
        }

        [HttpGet]
        public IActionResult AcceptRequest(AssetDTO asset)
        {
            var result = _adminService.AcceptRequest(asset);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The request has been Accepted successfully";
                return RedirectToAction("GetRequestedAssets");
            }
            TempData["acceptedmessage"] = "The request cannot be accepted";
            return RedirectToAction("GetRequestedAssets");
        }

        [HttpGet]
        public IActionResult DeclineRequest(AssetDTO asset)
        {
            var result = _adminService.DeclineRequest(asset);

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The request has been declined successfully";
                return RedirectToAction("GetRequestedAssets");
            }
            TempData["declinedmessage"] = "The request cannot be declined";
            return RedirectToAction("GetRequestedAssets");
        }
    }
}
