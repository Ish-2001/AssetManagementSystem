using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Logging;
using AssetManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AssetDetailsController : Controller
    {
        private readonly IAssetDetailsService _assetDetailService;
        private readonly ILog _logger;
        private readonly IConfiguration _configuration;

        public AssetDetailsController(IAssetDetailsService assetDetailService, ILog logger, IConfiguration configuration)
        {
            _logger = logger;
            _assetDetailService = assetDetailService;
            _configuration = configuration;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.Information("Displayed the list of assets successfully");
                return Ok(_assetDetailService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }

        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                _logger.Information("Displayed the list of assets categories successfully");
                return Ok(_assetDetailService.GetAllCategories());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AssetDetailsDTO newBook)
        {
            try
            {
                if (_assetDetailService.Add(newBook))
                {
                    _logger.Information("Asset Added Successfully");
                    return Ok("Asset added successfully");
                }
                else
                {
                    _logger.Warning("Asset is already present");
                    return BadRequest("Asset is already present");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }
        }

        [Route("{identityNumber}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string identityNumber)
        {
            try
            {
                if (_assetDetailService.Delete(identityNumber))
                {
                    _logger.Information("Asset deleted successfully\"");
                    return Ok("Asset deleted successfully");
                }
                else
                {
                    _logger.Warning("Asset not present");
                    return BadRequest("Asset not present");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }

        }

        [Route("{identityNumber}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(AssetDetailsDTO updatedAsset, string identityNumber)
        {
            try
            {
                if (_assetDetailService.Update(updatedAsset, identityNumber))
                {
                    _logger.Information("Asset Updated sucessfully");
                    return Ok("Asset Updated sucessfully");
                }
                else
                {
                    _logger.Warning("Warning is logged");
                    return BadRequest("Asset is not present");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error is logged");
                return Problem(ex.Message);
            }
        }
    }
}
