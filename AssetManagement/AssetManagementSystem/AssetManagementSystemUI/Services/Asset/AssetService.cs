using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AssetManagementSystemUI.Services.Asset
{
    public class AssetService
    {
        private readonly HelperAPI _helper = new HelperAPI();

        public async Task<List<AssetCategoryDTO>> GetAssetCategories()
        {
            HttpClient client = _helper.Initial();
            List<AssetCategoryDTO> assets = new List<AssetCategoryDTO>();

            HttpResponseMessage message = await client.GetAsync("AssetDetails/getallcategories");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<AssetCategoryDTO>>(result);
            }
            return assets;
        }

        public async Task<List<UserDTO>> GetUser()
        {
            HttpClient client = _helper.Initial();
            List<UserDTO> users = new List<UserDTO>();

            HttpResponseMessage message = await client.GetAsync("User/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<UserDTO>>(result);
            }
            return users;
        }

        public async Task<List<AssetDetailsDTO>> GetAssetDetails()
        {
            HttpClient client = _helper.Initial();
            List<AssetDetailsDTO> assets = new List<AssetDetailsDTO>();

            HttpResponseMessage message = await client.GetAsync("AssetDetails/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<AssetDetailsDTO>>(result);
            }
            return assets;
        }

        public List<string> GetAssetType(string filter)
        {
            if(filter == null)
                return null;

            var assets = GetAssetDetails().Result.Where(item => item.CategoryId == Convert.ToInt32(filter))
                  .Select(item => item.Type)
                  .Distinct()
                  .ToList();

            return assets;
        }

        public List<AssetDetailsDTO> GetAssets(string filter , string type)
        {
            if(filter == null || type == null) return null;

            var assets = GetAssetDetails().Result.Where(item => item.CategoryId == Convert.ToInt32(filter) && item.Type == type)
                        .Distinct()
                        .ToList();
            return assets;
        }
    }
}
