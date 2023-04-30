using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services.Asset;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Linq;
using System.Net.Http.Headers;

namespace AssetManagementSystemUI.Services.User
{
    public class UserService
    {
        private readonly HelperAPI _helper = new HelperAPI();

        private readonly AssetService _assetService;

        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public UserService(IHttpContextAccessor httpContextAccessor, AssetService assetService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("usertoken");
            _assetService = assetService;
        }

       
        public HttpResponseMessage Add(UserDTO user)
        {
            HttpClient client = _helper.Initial();
            var postTask = client.PostAsJsonAsync<UserDTO>("User/Add", user);
            postTask.Wait();

            return postTask.Result;
        }

        public HttpResponseMessage Login(UserLoginDTO login)
        {
            HttpClient client = _helper.Initial();
            var postTask = client.PostAsJsonAsync<UserLoginDTO>("User/UserLogin", login);
            postTask.Wait();

            return postTask.Result;

        }

        public HttpResponseMessage AssetRequest(AssetDTO asset,string userName)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            /*string identityNumber = _assetService.GetAssignedAssets().Result
                                          .Where(item => item.Id == asset.Id)
                                          .Select(item => item.AssetId)
                                          .FirstOrDefault();
*/
            AssetDetailsDTO assetDetails = _assetService.GetAssetDetails().Result
                                          .Where(item => item.IdentityNumber == asset.AssetName)
                                          .FirstOrDefault();

            AssetAssignmentDTO requestedAsset = new()
            {
                AssetType = _assetService.GetAssetCategories().Result.Where(i => i.Id == assetDetails.CategoryId)
                            .Select(i => i.Name)
                            .FirstOrDefault(),
                AssignDate = DateTime.Now,
                UnassignDate = null,
                Status = Status.Requested.ToString(),
                AssetId = asset.AssetName,
                UserId = _assetService.GetUser().Result.Where(item => item.UserName == userName)
                         .Select(item => item.Id).FirstOrDefault()
            };

            var postTask = client.PostAsJsonAsync<AssetAssignmentDTO>("asset/Assign", requestedAsset);
            postTask.Wait();

            return postTask.Result;
        }

        public async Task<List<AssetDTO>> GetRequests(string userName)
        {
            HttpClient client = _helper.Initial();
            List<AssetDTO> assets = new List<AssetDTO>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.GetAsync("asset/getall");

            UserDTO user = _assetService.GetUser().Result.Where(item => item.UserName == userName).FirstOrDefault();

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<AssetDTO>>(result);
               
                assets = assets.Where(item => item.UserId == user.Id).ToList(); 
            }

            foreach (var item in assets)
            {
                item.AssetName = _assetService.GetAssetDetails().Result.Where(i => i.IdentityNumber == item.AssetId)
                                .Select(i => i.Name).FirstOrDefault();
            }

            return assets;
        }

        public async void CancelRequest(int id)
        {

            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.DeleteAsync($"asset/cancelrequest/{id}");

        }       

    }
}
