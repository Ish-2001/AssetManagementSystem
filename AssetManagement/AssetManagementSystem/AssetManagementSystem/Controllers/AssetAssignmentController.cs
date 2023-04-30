using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Logging;
using AssetManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AssetManagementSystem.Controllers
{
    [ApiController]
    public class AssetAssignmentController : ControllerBase
    {
        private readonly IAssetAssignmentService _assetAssignmentService;
        private readonly ILog _logger;

        public AssetAssignmentController(IAssetAssignmentService assetAssignmentService , ILog logger)
        {
            _logger= logger;
            _assetAssignmentService = assetAssignmentService;
        }

        [HttpGet]
        [Route("asset/[action]")]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                _logger.Information("Displayed the list of assets successfully");
                return Ok(_assetAssignmentService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }

        }

        [HttpPost]
        [Route("asset/[action]")]
        [Authorize]
        public IActionResult Assign(AssetAssignmentDTO assetAssignment)
        {
            try
            {
                if (_assetAssignmentService.Assign(assetAssignment))
                {
                    _logger.Information("Asset is assigned successfully");
                    return Ok("Asset is assigned successfully");
                }
                else
                {
                    _logger.Warning("Asset cannot be assigned");
                    return BadRequest("Asset cannot be assigned");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("asset/[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult Unassign(AssetAssignmentDTO assetAssignment)
        {
            try
            {
                if (_assetAssignmentService.UnAssign(assetAssignment))
                {
                    _logger.Information("Asset unassigned successfully");
                    return Ok("Asset unassigned successfully");
                }
                else
                {
                    _logger.Warning("Asset cannot be unassigned");
                    return BadRequest("Asset cannot be unassigned");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("asset/[action]/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult CancelRequest(int id)
        {
            try
            {
                if (_assetAssignmentService.CancelRequest(id))
                {
                    _logger.Information("Information is logged");
                    return Ok("Request has been cancelled successfully");
                }
                else
                {
                    _logger.Warning("Warning is logged");
                    return BadRequest("Request cannot be cancelled");
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
