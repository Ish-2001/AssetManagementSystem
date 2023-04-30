using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services;
using AssetManagementSystemUI.Services.Admin;
using AssetManagementSystemUI.Services.Asset;
using AssetManagementSystemUI.Services.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Drawing.Text;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using static AssetManagementSystemUI.Models.DataPointDTO;

namespace AssetManagementSystemUI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly AssetService _assetService;
        private readonly IHttpContextAccessor _contextAccessor;
        public const string sessionToken = "usertoken";
        public const string sessionId = "username";

        public UserController(IHttpContextAccessor httpContextAccessor, UserService userService, AssetService assetService)
        {
            _userService = userService;
            _contextAccessor = httpContextAccessor;
            _assetService = assetService;
        }

        public IActionResult Index()
        {
            List<DataPointDTO> dataPoints = new List<DataPointDTO>();
            var assets = _assetService.GetAssetDetails().Result.Where(item => item.Quantity > 0)
                                     .GroupBy(item => item.CategoryId);

            foreach (var item in assets)
            {
                string name = _assetService.GetAssetCategories().Result.Where(i => i.Id == item.Key).Select(i => i.Name).FirstOrDefault();
                dataPoints.Add(new DataPointDTO(name, item.Count()));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserDTO user)
        {
            var result = _userService.Add(user);

            if (result.IsSuccessStatusCode)
            {
                ViewBag.message = "User Registered successfully";
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Remove("usertoken");
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginDTO login)
        {
            var result = _userService.Login(login);

            if (result.IsSuccessStatusCode)
            {
                var token = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString(sessionToken, token);
                HttpContext.Session.SetString(sessionId, login.UserName);
                return RedirectToAction("Index");
            }
            TempData["message"] = "Invalid credentials.Please enter the credentials again";
            return View();
        }

        [HttpGet]
        public IActionResult AssetRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AssetRequest(AssetDTO asset)
        {
            string userName = HttpContext.Session.GetString("username");
            var result = _userService.AssetRequest(asset, userName);
            TempData.Keep();

            if (result.IsSuccessStatusCode)
            {
                TempData["message"] = "The asset is requested successfully";
                return RedirectToAction("GetRequests");
            }
            TempData["message"] = "The asset could not be requested";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            string userName = HttpContext.Session.GetString("username");

            return View(await _userService.GetRequests(userName));
        }

        public IActionResult CancelRequest(int id)
        {
            try
            {
                _userService.CancelRequest(id);
                TempData["message"] = "The request has been cancelled successfully";
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
            }

            return RedirectToAction("GetRequests");
        }

    }
}
