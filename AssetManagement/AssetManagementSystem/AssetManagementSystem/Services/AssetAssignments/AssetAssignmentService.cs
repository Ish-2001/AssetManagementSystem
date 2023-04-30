using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;
using AssetManagementSystem.Services;

namespace AssetManagementSystem.Services
{
    public class AssetAssignmentService : IAssetAssignmentService
    {
        private readonly IAssetDetailsService _assetDetailsService;
        private readonly IUnitOfWork _unitOfWork;

        public AssetAssignmentService(IUnitOfWork unitOfWork, IAssetDetailsService assetDetailsService)
        {
            _unitOfWork = unitOfWork;
            _assetDetailsService = assetDetailsService;
        }

        public List<AssetAssignment> GetAll()
        {
            return _unitOfWork.AssetAssignments.GetAll();
        }

        public bool Assign(AssetAssignmentDTO assetAssignment)
        {
            User user = _unitOfWork.Users.Get(item => item.Id == assetAssignment.UserId);

            if (user == null) return false;

            bool assetExists = _unitOfWork.AssetCategories.Exists(item => item.Name == assetAssignment.AssetType);

            if(!assetExists) return false;

            return _assetDetailsService.Assign(assetAssignment);
        }

        public bool UnAssign(AssetAssignmentDTO assetAssignment)
        {
            User user = _unitOfWork.Users.Get(item => item.Id == assetAssignment.UserId);

            if (user == null)
            {
                return false;
            }

            bool assetExists = _unitOfWork.AssetCategories.Exists(item => item.Name == assetAssignment.AssetType);

            if (!assetExists) return false;

            return _assetDetailsService.UnAssign(assetAssignment);
        }

        public bool CancelRequest(int id)
        {
            /*User user = _unitOfWork.Users.Get(item => item.Id == assetAssignment.UserId);

            if (user == null)
            {
                return false;
            }

            bool assetExists = _unitOfWork.AssetCategories.Exists(item => item.Name == assetAssignment.AssetType);

            if (!assetExists) return false;*/

            return _assetDetailsService.CancelRequest(id);
        }
    }
}
