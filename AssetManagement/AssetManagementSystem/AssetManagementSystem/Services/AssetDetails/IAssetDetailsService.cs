using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.Services
{
    public interface IAssetDetailsService
    {
        bool Add(AssetDetailsDTO newBook);
        List<AssetDetail> GetAll();
        List<AssetCategory> GetAllCategories();
        bool Delete(string identityNumber);
        bool Update(AssetDetailsDTO updatedAsset, string identityNumber);
        bool Assign(AssetAssignmentDTO assetAssignment);
        bool UnAssign(AssetAssignmentDTO assetAssignment);
        bool CancelRequest(int id);
    }
}
