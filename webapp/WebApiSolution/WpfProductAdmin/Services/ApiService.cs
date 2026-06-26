using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using WpfProductAdmin.Models;

namespace WpfProductAdmin.Services
{
    public class ApiService
    {
        private readonly HttpClient client = new HttpClient();

        private readonly string serviceUrl = "http://localhost:5145/api/products";

        public async Task<ObservableCollection<Product>> GetProductsAsync()
        {
            try
            {
                // 공공데이터포털과 완전 동일
                //using HttpClient client = new HttpClient();

                string json = await client.GetStringAsync(serviceUrl);

                // 역직렬화
                var response = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);

                return response;
            }
            catch (Exception ex)
            {
                return new ObservableCollection<Product>();
            }
        }

        public async Task<bool> PostProductAsync(Product product)
        {
            var response = await client.PostAsJsonAsync(serviceUrl, product);

            return response.IsSuccessStatusCode;
        }
    }
}
