using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _dBContext;

        public UnitOfWork(DBContext dbcontext)
        {
            _dBContext = dbcontext;
            Users = new UserRepository(_dBContext);
            AssetAssignments = new AssetAssignmentRepository(_dBContext);
            AssetDetails = new AssetDetailRepository(_dBContext);
            AssetCategories = new AssetCategoryRepository(_dBContext);
        }

        public IUserRepository Users { get; }
        public IAssetAssignmentRepository AssetAssignments { get; }
        public IAssetDetailRepository AssetDetails { get; }
        public IAssetCategoryRepository AssetCategories { get; }

        public void Complete()
        {
            _dBContext.SaveChanges();
        }

    }
}
