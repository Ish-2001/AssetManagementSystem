using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        bool Add(UserDTO newUser);
        bool UserLogin(UserLoginDTO login);
        bool AdminLogin(AdminLoginDTO login);
    }
}
