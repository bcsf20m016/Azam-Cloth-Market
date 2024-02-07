using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface IProductRepo
    {
        public Task<ProductDTO> createProduct(ProductDTO product);
        public Task<int> updateProduct(ProductDTO product);
        public Task<int> deleteProduct(int id);
        public Task<IEnumerable<ProductDTO>> getAllProducts();
        public Task<IEnumerable<ProductDTO>> getAllProducts(int storeId);
        public Task<ProductDTO> GetProduct(int id, int storeId);
        public Task<IEnumerable<StoreDTO>> getStoresByProductName(string productName);
        public Task<IEnumerable<StoreDTO>> getStoresByProductPriceRange(int low, int high);
        public Task<ProductDTO> getProduct(int id);
        public Task<IEnumerable<ProductDTO>> getDiscountedProducts(int storeId);
        public Task<IEnumerable<ProductDTO>> getDiscountedCategoryProducts(int storeId);
        public Task<IEnumerable<ProductDTO>> getCategoryProducts(int categoryId);
        public Task<int> getProductCount(int storeId);
        public Task<int> clickProductCount(int productID);
    }
}