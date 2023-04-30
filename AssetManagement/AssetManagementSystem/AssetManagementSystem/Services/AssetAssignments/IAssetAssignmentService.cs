using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.Services
{
    public interface IAssetAssignmentService
    {
        bool Assign(AssetAssignmentDTO assetAssignment);
        bool UnAssign(AssetAssignmentDTO assetAssignment);
        bool CancelRequest(int id);
        List<AssetAssignment> GetAll();
    }
}
