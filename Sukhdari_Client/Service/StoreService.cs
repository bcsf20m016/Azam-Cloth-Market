using Models;
using Newtonsoft.Json;
using Sukhdari_Client.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sukhdari_Client.Service
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient _httpClient;
        public StoreService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<IEnumerable<StoreDTO>> getAllStores()
        {
            var response = await _httpClient.GetAsync($"api/Store/getAllStores");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }
        public async Task<IEnumerable<StoreDTO>> SearchStoreByCategory(string categoryName)
        {
            var response = await _httpClient.GetAsync($"api/Category/GetStoresByCategory/{categoryName}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }
        public async Task<IEnumerable<StoreDTO>> SearchStoreByProductName(string productName)
        {
            var response = await _httpClient.GetAsync($"api/Product/GetStoresByProductName/{productName}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }
        public async Task<IEnumerable<StoreDTO>> SearchStoreByProductPrice(int min, int max)
        {
            var response = await _httpClient.GetAsync($"api/Product/getStoresByPriceRange/{min}/{max}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }
        public async Task<StoreDTO> getStoreByName(string storeName)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoresByCategory/{storeName}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<StoreDTO>(content);
            return stores;
        }
        public async Task<IEnumerable<ProductDTO>> GetStoreProducts(int storeID)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoreProducts/{storeID}");
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
            return products;
        }
        public async Task<IEnumerable<StoreDTO>> getStoresByAllFilters(string data)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoresByAllFilters/{data}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }
        public async Task<IEnumerable<ProductDTO>> getAllProducts()
        {
            var response = await _httpClient.GetAsync($"api/Product/GetAllProducts");
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
            return products;
        }
        public async Task<IEnumerable<StoreDTO>> SearchStoreByCountry(string country)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoresByCountry/{country}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }
        public async Task<IEnumerable<ProductDTO>> getDiscountedProducts(int storeId)
        {
            var response = await _httpClient.GetAsync($"api/Product/getDiscountedProducts/{storeId}");
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
            return products;
        }
        public async Task<IEnumerable<CategoryDTO>> getStoreCategories(int storeId)
        {
            var response = await _httpClient.GetAsync($"api/Category/GetAllCategories/{storeId}");
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(content);
            return categories;
        }
        public async Task<IEnumerable<ProductDTO>> getCategoryProducts(int categoryId)
        {
            var response = await _httpClient.GetAsync($"api/Category/GetProductsWithCategoryID/{categoryId}");
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
            return products;
        }

        public async Task<ProductDTO> getSpecificProduct(int storeId, int productId)
        {
            var response = await _httpClient.GetAsync($"api/Product/getProduct/{storeId}/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(content);
            return product;
        }
        public async void AddCategoryClickCount(int categoryId)
        {
            var response = await _httpClient.GetAsync($"api/Category/AddCategoryClickCount/{categoryId}");
        }

        public async void AddStoreClickCount(int storeID)
        {
            var response = await _httpClient.GetAsync($"api/Store/AddStoreClickCount/{storeID}");
        }
        public async void AddProductClickCount(int productID)
        {
            var response = await _httpClient.GetAsync($"api/Product/AddProductClickCount/{productID}");
        }
        public async Task<IEnumerable<StoreDTO>> getStoresByTags(string tagName)
        {
            var response = await _httpClient.GetAsync($"api/StoreTag/GetStoresByTags/{tagName}");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreDTO>>(content);
            return stores;
        }
        public async Task<IEnumerable<StoreTagDTO>> getAllStoreTags()
        {
            var response = await _httpClient.GetAsync($"api/StoreTag/getAllStoreTags");
            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonConvert.DeserializeObject<IEnumerable<StoreTagDTO>>(content);
            return stores;
        }
        // I am adding code

        public async Task<StoreDTO> GetStoreByIdAsync(int storeId)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetStoreById/{storeId}");
            var content = await response.Content.ReadAsStringAsync();

            // Assuming the API returns a single StoreDTO for the given ID
            var store = JsonConvert.DeserializeObject<StoreDTO>(content);

            return store;
        }

        public async Task<String> getEmailByUserId(string userId)
        {
            var response = await _httpClient.GetAsync($"api/Store/GetEmailByUserId/{userId}");
            var email = await response.Content.ReadAsStringAsync();
            return email;
        }
}
        
}