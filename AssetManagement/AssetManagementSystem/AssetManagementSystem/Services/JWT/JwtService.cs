using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;
using AssetManagementSystem.Models;
using AssetManagementSystem.Services.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManagementSystem.Services
{
    public class JwtService : IJwtService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHashingService _hashingService;

        public JwtService
        (
           IUnitOfWork unitOfWork,
           IConfiguration configuration,
           IHashingService hashingService
        )
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _hashingService = hashingService;
        }

        public string GenerateTokenUser(string userName ,string password)
        {


            var user = _unitOfWork.Users.Find(u => u.UserName == userName);
                
            bool isValid = _hashingService.VerifyHashing(password , user.Password);

            if(!isValid )
            {
                return null;
            }

            return SignJwt(user);
        }

        public string GenerateTokenAdmin(string email, string password)
        {
            var user = _unitOfWork.Users.Find(u => u.Email == email);

            bool isValid = _hashingService.VerifyHashing(password, user.Password);

            if (!isValid)
            {
                return null;
            }

            return SignJwt(user);
        }

        private string SignJwt(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:TokenSecret"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                (!user.IsAdmin)? new Claim(ClaimTypes.NameIdentifier,user.UserName) : new Claim(ClaimTypes.NameIdentifier,user.Email) ,
                (!user.IsAdmin)? new Claim(ClaimTypes.Role,"User") : new Claim(ClaimTypes.Role,"Admin")
            };

            var token = new JwtSecurityToken(_configuration["JwtConfiguration:Issuer"],
                _configuration["JwtConfiguration:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credentials
                );
             
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
