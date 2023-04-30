using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services.Asset;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AssetManagementSystemUI.Services.SoftwareLicenseService
{
    public class SoftwareLicenseService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly AssetService _assetService;

        public SoftwareLicenseService(IHttpContextAccessor httpContextAccessor, AssetService assetService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
            _assetService = assetService;
        }
        public async Task<List<SoftwareLicenseDTO>> GetAll()
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            List<SoftwareLicenseDTO> licenses = new List<SoftwareLicenseDTO>();

            HttpResponseMessage message = await client.GetAsync("assetdetails/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                licenses = JsonConvert.DeserializeObject<List<SoftwareLicenseDTO>>(result);
                licenses = licenses.Where(item => item.CategoryId == (int)AssetId.SoftwareLicense).ToList();
            }

            return licenses;
        }

        public HttpResponseMessage Add(SoftwareLicenseDTO license)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            string serialId = _assetService.GetAssetCategories().Result.Where(item => item.Name == "Software License")
                              .Select(item => item.SerialId)
                              .FirstOrDefault();

            AssetDetailsDTO asset = new()
            {
                Name = license.Name,
                Type = license.Type,
                Source = license.Source,
                Date = license.Date,
                IdentityNumber = serialId + (new Random()).Next(100, 1000).ToString(),
                Quantity = license.Quantity,
                Price = license.Price,
                CategoryId = (int)AssetId.SoftwareLicense
            };

            var postTask = client.PostAsJsonAsync<AssetDetailsDTO>("assetdetails/Add", asset);
            postTask.Wait();

            return postTask.Result;
        }

        public async void Delete(string licenseNumber)
        {

            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.DeleteAsync($"assetdetails/delete/{licenseNumber}");
        }

        public HttpResponseMessage Edit(SoftwareLicenseDTO license, string identityNumber)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            AssetDetailsDTO asset = new()
            {
                IdentityNumber = identityNumber,
                Name = license.Name,
                Type = license.Type,
                Source = license.Source,
                Date = license.Date,
                Quantity = license.Quantity,
                Price = license.Price,
                CategoryId = (int)AssetId.SoftwareLicense
            };

            var putTask = client.PutAsJsonAsync<AssetDetailsDTO>($"assetdetails/update/{identityNumber}", asset);

            putTask.Wait();

            return putTask.Result;
        }

        public List<SoftwareLicenseDTO> Availability()
        {
            List<SoftwareLicenseDTO> softwares = GetAll().Result.Where(i => i.Quantity > 0).ToList();

            return softwares;
        }
    }
}
