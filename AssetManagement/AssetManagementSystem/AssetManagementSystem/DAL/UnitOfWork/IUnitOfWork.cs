using AssetManagementSystem.DAL;

namespace AssetManagementSystem.DAL
{
    public interface IUnitOfWork
    {
        void Complete();

        public IUserRepository Users { get; }
        public IAssetAssignmentRepository AssetAssignments { get; }
        public IAssetDetailRepository AssetDetails { get; }
        public IAssetCategoryRepository AssetCategories { get; }
    }
}
