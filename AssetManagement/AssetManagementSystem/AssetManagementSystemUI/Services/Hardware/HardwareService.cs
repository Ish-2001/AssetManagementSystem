using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services.Asset;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static System.Reflection.Metadata.BlobBuilder;

namespace AssetManagementSystemUI.Services
{
    public class HardwareService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly AssetService _assetService;

        public HardwareService(IHttpContextAccessor httpContextAccessor, AssetService assetService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _token = _session.GetString("admintoken");
            _assetService = assetService;
        }
        public async Task<List<HardwareDTO>> GetAll()
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            List<HardwareDTO> hardwares = new List<HardwareDTO>();

            HttpResponseMessage message = await client.GetAsync("assetdetails/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                hardwares = JsonConvert.DeserializeObject<List<HardwareDTO>>(result);
                hardwares = hardwares.Where(item => item.CategoryId == (int)AssetId.Hardware).ToList();
            }

            return hardwares;
        }

        public HttpResponseMessage Add(HardwareDTO hardware)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

			string serialId = _assetService.GetAssetCategories().Result.Where(item => item.Name == AssetId.Hardware.ToString())
							  .Select(item => item.SerialId)
							  .FirstOrDefault();

			AssetDetailsDTO asset = new()
            {
                Name = hardware.Name,
                Type = hardware.Type,
                Source = hardware.Source,
                Date = hardware.Date,
                IdentityNumber = serialId + (new Random()).Next(100, 1000).ToString(),
                Quantity = hardware.Quantity,
                Price = hardware.Price,
                CategoryId = (int)AssetId.Hardware
            };

            var postTask = client.PostAsJsonAsync<AssetDetailsDTO>("assetdetails/Add", asset);
            postTask.Wait();

            return postTask.Result;           
        }

        public async void Delete(string modelNumber)
        {

            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.DeleteAsync($"assetdetails/delete/{modelNumber}");
        }

        public HttpResponseMessage Edit(HardwareDTO hardware, string identityNumber)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            AssetDetailsDTO asset = new()
            {
                IdentityNumber = identityNumber,
                Name = hardware.Name,
                Type = hardware.Type,
                Source = hardware.Source,
                Date = hardware.Date,
                Quantity = hardware.Quantity,
                Price = hardware.Price,
                CategoryId = (int)AssetId.Book
            };

            var putTask = client.PutAsJsonAsync<AssetDetailsDTO>($"assetdetails/update/{identityNumber}", asset);

            putTask.Wait();

            return putTask.Result;
        }

        public List<HardwareDTO> Availability()
        {
            List<HardwareDTO> hardwares = GetAll().Result.Where(i => i.Quantity > 0).ToList();

            return hardwares;
        }
    }
}
