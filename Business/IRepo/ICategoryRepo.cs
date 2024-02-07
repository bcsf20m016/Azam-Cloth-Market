using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepo
{
    public interface ICategoryRepo
    {
        public Task<int> createCategory(CategoryDTO category);
        public Task<int> deleteCategory(int id);
        public Task<int> updateCategory(CategoryDTO category);
        public Task<IEnumerable<CategoryDTO>> GetAllCategories(int StoreId);
        public Task<IEnumerable<CategoryDTO>> GetAllCategories();
        public Task<CategoryDTO> GetCategory(int id, int storeId);
        public Task<IEnumerable<StoreDTO>> getStoreByCategory(string categoryName);
        public Task<IEnumerable<CategoryDTO>> GetAllCategoriesWithProducts(int StoreId);
        public Task<IEnumerable<CategoryDTO>> getDicountedCategory(int storeId);
        public Task<CategoryDTO> getCategoryByName(string categoryName);
        public Task<int> getCategoryCount(int storeID);
        public Task<int> clickCategoryCount(int categoryID);

    }
}