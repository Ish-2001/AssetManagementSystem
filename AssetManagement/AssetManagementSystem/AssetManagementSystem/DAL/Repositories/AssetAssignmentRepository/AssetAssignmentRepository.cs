using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.DAL
{
    public class AssetAssignmentRepository : GenericRepository<AssetAssignment> , IAssetAssignmentRepository
    {
        public AssetAssignmentRepository(DBContext _context) : base(_context)
        {
        }
    }
}
