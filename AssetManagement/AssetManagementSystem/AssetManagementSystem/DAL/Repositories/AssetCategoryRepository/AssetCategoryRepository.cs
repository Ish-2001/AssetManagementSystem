using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.DAL
{
    public class AssetCategoryRepository : GenericRepository<AssetCategory>, IAssetCategoryRepository
    {
        public AssetCategoryRepository(DBContext _context) : base(_context)
        {

        }
    }
}
