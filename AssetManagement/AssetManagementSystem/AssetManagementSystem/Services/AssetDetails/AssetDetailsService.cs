using AssetManagementSystem.DAL;
using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;

namespace AssetManagementSystem.Services
{
    public class AssetDetailsService : IAssetDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssetDetailsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public List<AssetDetail> GetAll()
        {
            return _unitOfWork.AssetDetails.GetAll();
        }

        public List<AssetCategory> GetAllCategories()
        {
            return _unitOfWork.AssetCategories.GetAll();
        }

        public bool Add(AssetDetailsDTO newAsset)
        {
            bool assetExists = _unitOfWork.AssetDetails.Exists(item => item.IdentityNumber == newAsset.IdentityNumber);

            if (assetExists) return false;

            AssetDetail asset = new()
            {
                Name = newAsset.Name,
                Source = newAsset.Source,
                Type= newAsset.Type,
                Date = newAsset.Date,
                Quantity = newAsset.Quantity,
                Price = newAsset.Price,
                CategoryId = newAsset.CategoryId,
                IdentityNumber = newAsset.IdentityNumber
            };

            _unitOfWork.AssetDetails.Add(asset);
            _unitOfWork.Complete();
            return true;

        }

        public bool Delete(string identityNumber)
        {
            AssetDetail asset = _unitOfWork.AssetDetails.Get(item => item.IdentityNumber == identityNumber);
            /*AssetAssignment assignment = _unitOfWork.AssetAssignments.Get(item => item.AssetId == identityNumber);

            if(assignment != null) {
                if (assignment.Status == Status.Assigned.ToString() || assignment.Status == Status.Accepted.ToString()) return false;
            }  */        

            if (asset == null) return false;

            _unitOfWork.AssetDetails.Delete(asset);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(AssetDetailsDTO updatedAsset, string identityNumber)
        {
            AssetDetail asset = _unitOfWork.AssetDetails.Get(item => item.IdentityNumber == identityNumber);

            if (asset == null) return false;

            asset.Name = updatedAsset.Name;
            asset.Source = updatedAsset.Source;
            asset.Date = updatedAsset.Date;
            asset.Quantity = updatedAsset.Quantity;
            asset.Price = updatedAsset.Price;
            asset.Type = updatedAsset.Type;

            _unitOfWork.AssetDetails.Update(asset);
            _unitOfWork.Complete();
            return true;

        }

        public bool Assign(AssetAssignmentDTO assetAssignment)
        {
            AssetAssignment assignment;

            if (assetAssignment.Status == Status.Assigned.ToString())
            {
                assignment = _unitOfWork.AssetAssignments
                    .Find(item => item.UserId == assetAssignment.UserId && item.AssetType == assetAssignment.AssetType
                    && item.AssetId == assetAssignment.AssetId && item.Status == Status.Assigned.ToString());
            }
            else
            {
                assignment = _unitOfWork.AssetAssignments
                    .Find(item => item.UserId == assetAssignment.UserId && item.AssetType == assetAssignment.AssetType
                    && item.AssetId == assetAssignment.AssetId && item.Status == Status.Requested.ToString());
            }
                

            AssetDetail asset = _unitOfWork.AssetDetails.Get(item => item.IdentityNumber == assetAssignment.AssetId);

            if (asset == null) return false;

            if(assetAssignment.Status == Status.Assigned.ToString())
            {
                if (asset.Quantity <= 0) return false;
            }

            if(assetAssignment.Status == Status.Assigned.ToString())
                if (assignment != null) return false;

            if (assetAssignment.Status == Status.Accepted.ToString() || assetAssignment.Status == Status.Declined.ToString())
                assignment.Status = assetAssignment.Status;

            if(assetAssignment.Status == Status.Assigned.ToString() || assetAssignment.Status == Status.Requested.ToString())
            {
                AssetAssignment assetToBeAssigned = new AssetAssignment()
                {
                    AssetId = assetAssignment.AssetId,
                    AssetType = assetAssignment.AssetType,
                    UserId = assetAssignment.UserId,
                    AssignDate = DateTime.Now,
                    Status = assetAssignment.Status

                };

                _unitOfWork.AssetAssignments.Add(assetToBeAssigned);
            }
            

            if (assetAssignment.Status == Status.Assigned.ToString() || assetAssignment.Status == Status.Accepted.ToString())
            {
                asset.Quantity -= 1;
                _unitOfWork.AssetDetails.Update(asset);
            }
               
          
            _unitOfWork.Complete();

            return true;
        }

        public bool UnAssign(AssetAssignmentDTO assetAssignment)
        {
            var assignment = _unitOfWork.AssetAssignments.Find(item => item.UserId == assetAssignment.UserId 
                             && item.AssetType == assetAssignment.AssetType
                             && item.AssetId == assetAssignment.AssetId && item.Status == Status.Assigned.ToString());

            AssetDetail asset = _unitOfWork.AssetDetails.Get(item => item.IdentityNumber == assetAssignment.AssetId);

            if (assignment == null) return false;

            asset.Quantity += 1;
            assignment.Status = Status.Unassigned.ToString();
            assignment.UnassignDate = DateTime.Now;

            _unitOfWork.AssetAssignments.Update(assignment);
            _unitOfWork.Complete();

            return true;
        }

        public bool CancelRequest(int id)
        {
            var asset = _unitOfWork.AssetAssignments.Find(item => item.Id == id);

            if (asset == null) return false;

            _unitOfWork.AssetAssignments.Delete(asset);
            _unitOfWork.Complete();

            return true;
        }
    }
}
