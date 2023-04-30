using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.DAL
{
    public class AssetDetailRepository : GenericRepository<AssetDetail>, IAssetDetailRepository
    {
        public AssetDetailRepository(DBContext _context) : base(_context)
        {

        }
    }
}
