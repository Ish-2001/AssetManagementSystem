using AssetManagementSystem.Data.Domain;

namespace AssetManagementSystem.Services.JWT
{
    public interface IJwtService
    {
        public string GenerateTokenUser(string userName, string password);
        public string GenerateTokenAdmin(string email, string password);
    }
}
