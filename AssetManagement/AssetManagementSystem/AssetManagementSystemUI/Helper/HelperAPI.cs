namespace AssetManagementSystemUI.Helper
{
    public class HelperAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7057/");
            return client;
        }
    }
}
