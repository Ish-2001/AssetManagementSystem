using AssetManagementSystemUI.Helper;
using AssetManagementSystemUI.Models;
using AssetManagementSystemUI.Services.Asset;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AssetManagementSystemUI.Services
{
    public class BookService
    {
        private readonly HelperAPI _helper = new HelperAPI();
        private string _token { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly AssetService _assetService;

        public BookService(IHttpContextAccessor httpContextAccessor , AssetService assetService)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _assetService = assetService;
            _token = _session.GetString("admintoken");
        }

        public async Task<List<BookDTO>> GetAll()
        {
            HttpClient client = _helper.Initial();
            List<BookDTO> books = new List<BookDTO>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.GetAsync("AssetDetails/getall");

            if (message.IsSuccessStatusCode)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookDTO>>(result);
                books = books.Where(item => item.CategoryId == (int)AssetId.Book).ToList();
            }
            return books;
        }

        public HttpResponseMessage Add(BookDTO book)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            string serialId = _assetService.GetAssetCategories().Result.Where(item => item.Name == AssetId.Book.ToString())
                              .Select(item => item.SerialId)
                              .FirstOrDefault();

            AssetDetailsDTO asset = new()
            {
                Name = book.Name,
                Type = book.Type,
                Source = book.Source,
                Date = book.Date,
                IdentityNumber = serialId + (new Random()).Next(100, 1000).ToString(),
                Quantity = book.Quantity,
                Price= book.Price,
                CategoryId = (int)AssetId.Book
            };

            var postTask = client.PostAsJsonAsync<AssetDetailsDTO>("AssetDetails/Add", asset);
            postTask.Wait();

            return postTask.Result;
            
        }

        public async void Delete(string serialNumber)
        {

            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            HttpResponseMessage message = await client.DeleteAsync($"assetdetails/delete/{serialNumber}");
        }

        public HttpResponseMessage Edit(BookDTO book, string identityNumber)
        {
            HttpClient client = _helper.Initial();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            AssetDetailsDTO asset = new()
            {
                IdentityNumber = identityNumber,
                Name = book.Name,
                Type = book.Type,
                Source = book.Source,
                Date = book.Date,
                Quantity = book.Quantity,
                Price = book.Price,
                CategoryId= (int)AssetId.Book
            };

            var putTask = client.PutAsJsonAsync<AssetDetailsDTO>($"AssetDetails/update/{identityNumber}", asset);

            putTask.Wait();

            return putTask.Result;
        }

        public List<BookDTO> Availability()
        {
           List<BookDTO> books = GetAll().Result.Where(i=>i.Quantity > 0).ToList();

            return books;
        }
    }
}
