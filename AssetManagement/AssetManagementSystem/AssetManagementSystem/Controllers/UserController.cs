using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;
using AssetManagementSystem.Logging;
using AssetManagementSystem.Services;
using AssetManagementSystem.Services.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AssetManagementSystem.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILog _logger;
       // private readonly IConfiguration _configuration;

        public UserController(IUserService userService, ILog logger, IJwtService jwtService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.Information("Displayed the list of users successfully");
                return Ok(_userService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Add(UserDTO newUser)
        {
            try
            {
                if (_userService.Add(newUser))
                {
                    _logger.Information("User has been registered Successfully");
                    return Ok("User has been Registered successfully");
                }
                else
                {
                    _logger.Warning("User is already present");
                    return BadRequest("User is already present");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        //[Authorize]
        public IActionResult UserLogin(UserLoginDTO login)
        {
            try
            {
                if (_userService.UserLogin(login))
                {
                    _logger.Information("User has been logged in Successfully");

                    string token = _jwtService.GenerateTokenUser(login.UserName,login.Password);
                    //string token = SignJwt(login);
                    return Ok(token);
                }
                else
                {
                    _logger.Warning("User is not present");
                    return Unauthorized("User is not present");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult AdminLogin(AdminLoginDTO login)
        {
            try
            {
                if (_userService.AdminLogin(login))
                {
                    _logger.Information("Admin has been logged in Successfully");
                    string token = _jwtService.GenerateTokenAdmin(login.Email, login.Password);
                    //string token = SignJwt(login);
                    return Ok(token);
                }
                else
                {
                    _logger.Warning("Admin is not present");
                    return BadRequest("Admin is not present");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
