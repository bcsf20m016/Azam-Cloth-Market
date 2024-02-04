using Business.IRepo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IProductRepo _productRepo;

        public CategoryController(ICategoryRepo categoryRepo, IProductRepo productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetAllCategories(int? storeId)
        {
            var categories = await _categoryRepo.GetAllCategories(storeId.Value);
            return Ok(categories);
        }

        [HttpGet("{id},{storeId}")]
        public async Task<IActionResult> GetCategory(int? id, int? storeId)
        {
            var category = await _categoryRepo.GetCategory(id.Value, storeId.Value);
            return Ok(category);
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetProductsWithCategoryName(string categoryName)
        {
            var products = await _productRepo.getAllProducts();
            var category = await _categoryRepo.GetAllCategories();

            var categoryToFind = category.FirstOrDefault(i => i.Name.ToLower().Contains(categoryName.ToLower()));
            var productsToFind = products.Where(x => x.CategoryId == categoryToFind.Id);
            //var categoryToFind = category.Where(x => x.Name.Trim().ToLower().Contains(categoryName.ToLower())).Select(x => x.Id);
            //var productsToFind = products.All(x => products.Any(a => a.CategoryId.Equals(categoryToFind)));
            return Ok(productsToFind);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetProductsWithCategoryID(int categoryId)
        {
            var products = await _productRepo.getAllProducts();
            var productsToFind = products.Where(x => x.CategoryId == categoryId);
            return Ok(productsToFind);
        }
        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetStoresByCategory(string categoryName)
        {
            var stores = await _categoryRepo.getStoreByCategory(categoryName);
            return Ok(stores);
        }
        [HttpGet("{storeId}")]
        public async Task<IActionResult> getDiscountedCategoryProducts(int storeId)
        {
            var products = await _productRepo.getDiscountedCategoryProducts(storeId);
            return Ok(products);
        }
        [HttpGet("{categoryID}")]
        public async Task<IActionResult> AddCategoryClickCount(int categoryID)
        {
            var res = await _categoryRepo.clickCategoryCount(categoryID);
            return Ok();
        }
    }
}