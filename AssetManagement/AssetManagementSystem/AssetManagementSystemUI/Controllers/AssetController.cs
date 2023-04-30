using AssetManagementSystemUI.Services;
using AssetManagementSystemUI.Services.Asset;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace AssetManagementSystemUI.Controllers
{
    public class AssetController : Controller
    {
        /*private readonly IHttpContextAccessor _contextAccessor;
        private readonly AssetService _assetService;

        public AssetController(IHttpContextAccessor httpContextAccessor, AssetService assetService)
        {
            _contextAccessor = httpContextAccessor;
            _assetService = assetService;
        }*/

        private readonly AssetService _assetService = new AssetService();

        [HttpGet]
        public IActionResult GetAssetType(string filter)
        {
            return Json(_assetService.GetAssetType(filter));
        }

        [HttpGet]
        public IActionResult GetAssets(string filter , string type)
        {
            return Json(_assetService.GetAssets(filter, type));
        }
    }
}
