using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;
using AssetManagementSystem.Services.JWT;

namespace AssetManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public UserService(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public List<User> GetAll()
        {
            return _unitOfWork.Users.GetAll();
        }

        public bool Add(UserDTO newUser)
        {
            bool userExists = _unitOfWork.Users.Exists(item => item.Password == newUser.Password && item.UserName == newUser.UserName);

            if (userExists) return false;

            User user = new()
            {
                UserName = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                DateOfBirth = newUser.DateOfBirth,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Password = _hashingService.Hashing(newUser.Password),
                IsAdmin = newUser.IsAdmin
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
            return true;

        }

        public bool UserLogin(UserLoginDTO login)
        {
            User user = _unitOfWork.Users.Get(item => item.UserName == login.UserName);

            if (user == null) return false;

            if (user.IsAdmin) return false;

            bool isValidPassword = _hashingService.VerifyHashing(login.Password, user.Password);

            if(!isValidPassword)
                return false;

            return true;
        }

        public bool AdminLogin(AdminLoginDTO login)
        {
            User user = _unitOfWork.Users.Get(item => item.Email == login.Email);

            if (user == null) return false;

            if(!user.IsAdmin) return false;

            bool isValidPassword = _hashingService.VerifyHashing(login.Password, user.Password);

            if (!isValidPassword)
                return false;

            return true;
        }
    }
}
