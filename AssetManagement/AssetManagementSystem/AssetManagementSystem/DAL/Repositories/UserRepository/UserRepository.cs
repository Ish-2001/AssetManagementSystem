using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.DAL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DBContext _context) : base(_context)
        {
        }
    }
}
