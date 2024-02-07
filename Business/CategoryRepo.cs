using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepo(ApplicationDbContext db, IMapper map)
        {
            _db = db;
            _mapper = map;
        }
        public async Task<int> createCategory(CategoryDTO category)
        {
            var categories = await _db.Categories.FirstOrDefaultAsync(i => i.Name.ToLower() == category.Name.ToLower() && i.StoreId == category.StoreId);
            if(category.Id == 0 && categories != null)
            {
                return 0;
            }
            else if (category.Id != 0)
            {
                var oldCategory = _db.Categories.Include(i=> i.Attributes).FirstOrDefault(i => i.Id == category.Id);
                //if (categories != null && oldCategory.Name.ToLower() == category.Name.ToLower())
                //{
                //    oldCategory.Name = category.Name;
                //}
                //else if (categories != null && oldCategory.Name.ToLower() != category.Name.ToLower())
                //{
                //    return 0;
                //}
                oldCategory.Name = category.Name;
                oldCategory.Description = category.Description;
                if (category.DiscountPercentage >= 0 && category.DiscountPercentage <= 100)
                {
                    oldCategory.DiscountPercentage = category.DiscountPercentage;
                }
                var products = _db.Products.Where(i => i.CategoryId == category.Id).ToList();
                if (category.DiscountPercentage != null)
                {
                    if (products != null && products.Any())
                    {
                        foreach (var p in products)
                        {
                            if (category.DiscountPercentage >= 0 && category.DiscountPercentage <= 100)
                            {
                                p.DiscountPercentage = category.DiscountPercentage;
                                p.DiscountPrice = p.Price - ((category.DiscountPercentage / 100) * p.Price);
                            }
                        }
                    }
                }
                else
                {
                    if (products != null && products.Any())
                    {
                        foreach (var p in products)
                        {
                            p.DiscountPercentage = null;
                            p.DiscountPrice = null;
                        }
                    }
                }

                if(oldCategory.Attributes != null)
                {
                    oldCategory.Attributes.Clear();
                    foreach(var attrib in category.Attributes)
                    {
                        var attribute = new CategoryAttributes
                        {
                            Key = attrib.Key
                        };
                        oldCategory.Attributes.Add(attribute);
                    }
                }

                oldCategory.ClickCount = category.ClickCount;
                return await _db.SaveChangesAsync();

            }

            Category newCategory = _mapper.Map<CategoryDTO, Category>(category);
            newCategory.ClickCount = 0;
            await _db.Categories.AddAsync(newCategory);
            return await _db.SaveChangesAsync();

        }
        public async Task<CategoryDTO> getCategoryByName(string categoryName)
        {
            var categories = await _db.Categories.FirstOrDefaultAsync(i => i.Name.ToLower() == categoryName.ToLower());
            return _mapper.Map<Category, CategoryDTO>(categories);
        }
        public async Task<int> deleteCategory(int id)
        {
            Category category = await _db.Categories.FindAsync(id);
            if (category != null)
            {
                if (category.DiscountPercentage != null)
                {
                    var products = _db.Products.Where(i => i.CategoryId == category.Id).ToList();
                    if (products != null && products.Any())
                    {
                        foreach (var p in products)
                        {
                            p.DiscountPercentage = null;
                            p.DiscountPrice = null;
                        }
                    }
                    await _db.SaveChangesAsync();
                }
                _db.Categories.Remove(category);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<CategoryDTO> GetCategory(int id, int storeId)
        {
            var category = await _db.Categories.Include(i=> i.Attributes).FirstOrDefaultAsync(i => i.Id == id && i.StoreId == storeId);
            return _mapper.Map<Category, CategoryDTO>(category);
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategories(int StoreId)
        {
            IEnumerable<CategoryDTO> dto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories.Where(i => i.StoreId == StoreId).Include(i=>i.Attributes));
            return dto;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            IEnumerable<CategoryDTO> dto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
            return dto;
        }
        public async Task<int> updateCategory(CategoryDTO category)
        {
            Category oldCategory = await _db.Categories.FindAsync(category.Id);
            Category newCategory = _mapper.Map<CategoryDTO, Category>(category, oldCategory);
            _db.Categories.Update(newCategory);
            return await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<StoreDTO>> getStoreByCategory(string categoryName)
        {
            var storeCategories = _db.Categories.Where(i => i.Name.ToLower().Contains(categoryName.ToLower())).ToList();
            List<Store> storeByCategoryList = new List<Store>();
            foreach (var s in storeCategories)
            {
                storeByCategoryList.Add(await _db.Stores.Include(i => i.StoreImages).FirstOrDefaultAsync(i => i.Id == s.StoreId));
            }
            return _mapper.Map<IEnumerable<Store>, IEnumerable<StoreDTO>>(storeByCategoryList);
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesWithProducts(int StoreId)
        {
            var things = await _db.Categories.Include(i => i.Products).ThenInclude(i => i.ProductImages).Where(i => i.StoreId == StoreId).ToListAsync();
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(things);
        }
        public async Task<IEnumerable<CategoryDTO>> getDicountedCategory(int storeId)
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(await _db.Categories.Where(i => i.DiscountPercentage != null).ToListAsync());
        }
        public async Task<int> getCategoryCount(int storeID)
        {
            var count = _db.Categories.Where(i => i.StoreId == storeID).Count();
            return count;
        }
        public async Task<int> clickCategoryCount(int categoryID)
        {
            var category = _db.Categories.FirstOrDefault(i => i.Id == categoryID);
            if (category.ClickCount == null)
            {
                category.ClickCount = 1;
            }
            else
            {
                category.ClickCount++;
            }
            return await _db.SaveChangesAsync();
        }
    }
}