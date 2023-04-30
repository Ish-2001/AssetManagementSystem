using BC = BCrypt.Net.BCrypt;

namespace AssetManagementSystem.Services
{
    public class HashingService : IHashingService
    {
        public string Hashing(string password)
        {
            string reducedPassword = BC.HashPassword(password);
            return reducedPassword;
        }

        public bool VerifyHashing(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
