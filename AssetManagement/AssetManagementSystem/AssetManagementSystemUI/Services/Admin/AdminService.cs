using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services.Asset;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Net.Http.Headers;

namespace AssetManagementSystemUI.Services.Admin
{
    public class AdminService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private readonly AssetService _assetService;

        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public AdminService(IHttpContextAccessor httpContextAccessor,AssetService assetService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
            _assetService = assetService;
        }

        public HttpResponseMessage Login(AdminLoginDTO login)
        {
            HttpClient client = _helper.Initial();
            var postTask = client.PostAsJsonAsync<AdminLoginDTO>("User/AdminLogin", login);
            postTask.Wait();

            return postTask.Result;
        }

        public HttpResponseMessage Assign(AssetDTO asset)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            bool isUserExists = _assetService.GetUser().Result.Any(item => item.UserName == asset.UserName);

            if (!isUserExists) return null;

            AssetDetailsDTO assetDetails = _assetService.GetAssetDetails().Result
                                          .Where(item => item.IdentityNumber == asset.AssetName)
                                          .FirstOrDefault();

            if (assetDetails.Quantity <= 0) return null;

            AssetAssignmentDTO assignedAsset = new()
            {
                AssetType = _assetService.GetAssetCategories().Result.Where(i => i.Id == assetDetails.CategoryId)
                            .Select(i => i.Name)
                            .FirstOrDefault(),
                AssignDate = DateTime.Now,
                UnassignDate = null,
                Status = Status.Assigned.ToString(),
                AssetId = asset.AssetName,
                UserId = _assetService.GetUser().Result.Where(item => item.UserName == asset.UserName)
                         .Select(item => item.Id).FirstOrDefault()
            };

            var postTask = client.PostAsJsonAsync<AssetAssignmentDTO>("asset/Assign", assignedAsset);
            postTask.Wait();

            return postTask.Result;
        }

        public HttpResponseMessage Unassign(AssetDTO asset)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            string identityNumber = GetAssignedAssets().Result
                                          .Where(item => item.Id == asset.Id)
                                          .Select(item => item.AssetId)
                                          .FirstOrDefault();

            AssetAssignmentDTO unassignedAsset = new()
            {
                AssetType = asset.AssetType,
                UnassignDate = DateTime.Now,
                Status = Status.Unassigned.ToString(),
                AssetId = identityNumber,
                UserId = _assetService.GetUser().Result.Where(item => item.UserName == asset.UserName)
                         .Select(item => item.Id)
                         .FirstOrDefault()
            };

            var postTask = client.PutAsJsonAsync<AssetAssignmentDTO>("asset/Unassign", unassignedAsset);
            postTask.Wait();

            return postTask.Result;
        }

        public async Task<List<AssetDTO>> GetAssignedAssets()
        {
            HttpClient client = _helper.Initial();
            List<AssetDTO> assets = new List<AssetDTO>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.GetAsync("asset/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<AssetDTO>>(result);
            }
            else if (message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }

            foreach (var item in assets)
            {
                item.AssetName = _assetService.GetAssetDetails().Result.Where(i => i.IdentityNumber == item.AssetId)
                                .Select(i => i.Name).FirstOrDefault();
                item.FirstName = _assetService.GetUser().Result.Where(i => i.Id == item.UserId)
                                .Select(i => i.FirstName)
                                .FirstOrDefault();
            }

            return assets;
        }

        public async Task<List<AssetDTO>> GetRequestedAssets()
        {
            HttpClient client = _helper.Initial();
            List<AssetDTO> assets = new List<AssetDTO>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.GetAsync("asset/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<AssetDTO>>(result);
                assets = assets.Where(item => item.Status == Status.Requested.ToString()).ToList();
            }
            else if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }

            foreach (var item in assets)
            {
                item.AssetName = _assetService.GetAssetDetails().Result.Where(i => i.IdentityNumber == item.AssetId)
                                .Select(i => i.Name).FirstOrDefault();
                var categoryId = _assetService.GetAssetDetails().Result.Where(i => i.IdentityNumber == item.AssetId)
                                 .Select(i => i.CategoryId).FirstOrDefault();
                item.UserName = _assetService.GetUser().Result.Where(i => i.Id == item.UserId)
                                .Select(i => i.UserName)
                                .FirstOrDefault();
                item.Quantity = (int)_assetService.GetAssetDetails().Result.Where(i => i.IdentityNumber == item.AssetId)
                                .Select(i => i.Quantity).FirstOrDefault();
            }

            return assets;
        }

        public HttpResponseMessage AcceptRequest(AssetDTO asset)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            bool isUserExists = _assetService.GetUser().Result.Any(item => item.UserName == asset.UserName);

            if (!isUserExists) return null;

            AssetDetailsDTO assetDetails = _assetService.GetAssetDetails().Result
                                          .Where(item => item.IdentityNumber == asset.AssetId)
                                          .FirstOrDefault();

            if (assetDetails.Quantity <= 0) return null;

            AssetAssignmentDTO assignedAsset = new()
            {
                AssetType = _assetService.GetAssetCategories().Result.Where(i => i.Id == assetDetails.CategoryId)
                            .Select(i => i.Name)
                            .FirstOrDefault(),
                AssignDate = DateTime.Now,
                UnassignDate = null,
                Status = Status.Accepted.ToString(),
                AssetId = asset.AssetId,
                UserId = _assetService.GetUser().Result.Where(item => item.UserName == asset.UserName)
                         .Select(item => item.Id).FirstOrDefault()
            };

            var postTask = client.PostAsJsonAsync<AssetAssignmentDTO>("asset/Assign", assignedAsset);
            postTask.Wait();

            return postTask.Result;
        }

        public HttpResponseMessage DeclineRequest(AssetDTO asset)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            bool isUserExists = _assetService.GetUser().Result.Any(item => item.UserName == asset.UserName);

            if (!isUserExists) return null;

            AssetDetailsDTO assetDetails = _assetService.GetAssetDetails().Result
                                          .Where(item => item.IdentityNumber == asset.AssetId)
                                          .FirstOrDefault();

            AssetAssignmentDTO assignedAsset = new()
            {
                AssetType = _assetService.GetAssetCategories().Result.Where(i => i.Id == assetDetails.CategoryId)
                            .Select(i => i.Name)
                            .FirstOrDefault(),
                AssignDate = DateTime.Now,
                UnassignDate = null,
                Status = Status.Declined.ToString(),
                AssetId = asset.AssetId,
                UserId = _assetService.GetUser().Result.Where(item => item.UserName == asset.UserName)
                         .Select(item => item.Id).FirstOrDefault()
            };

            var postTask = client.PostAsJsonAsync<AssetAssignmentDTO>("asset/Assign", assignedAsset);
            postTask.Wait();

            return postTask.Result;
        }
    }
}
