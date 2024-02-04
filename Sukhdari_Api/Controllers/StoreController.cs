using Business.IRepo;
using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StoreController : Controller
    {
        private readonly IStoreRepo _storeRepo;
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ICountDetailsRepo _countDetailsRepo;
        public StoreController(IStoreRepo storeRepo, IProductRepo productRepo, ICategoryRepo categoryRepo, ICountDetailsRepo countRepo)
        {

            _storeRepo = storeRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _countDetailsRepo = countRepo;
        }

        [HttpGet]
        public async Task<IActionResult> getAllStores()
        {
            var allStores = await _storeRepo.getAllStores();
            return Ok(allStores);
        }

        [HttpGet("{StoreName}")]
        public async Task<IActionResult> GetProductsWithStoreName(string StoreName)
        {
            var store = _storeRepo.GetStoreByName(StoreName.ToLower().Trim());
            if (store == null)
            {
                return BadRequest(new ErrorModelDTO() { ErrorMessage = "No such Store Exist (", Title = "", StatusCode = StatusCodes.Status404NotFound });

            }
            var s_Id = store.Id;
            //var products = await _productRepo.getAllProducts();
            //var productsToFind = products.FirstOrDefault(i => i.StoreId == s_Id);
            var productsToFind = await _productRepo.getAllProducts(s_Id);
            return Ok(productsToFind);
        }

        [HttpGet("{StoreID}")]
        public async Task<IActionResult> GetStoreProducts(int StoreID)
        {
            var products = await _productRepo.getAllProducts(StoreID);
            if (products == null)
            {
                return BadRequest(new ErrorModelDTO() { ErrorMessage = "No Products Exist (", Title = "", StatusCode = StatusCodes.Status404NotFound });
            }
            return Ok(products);
        }

        [HttpGet("{filterData}")]
        public async Task<IActionResult> GetStoresByAllFilters(string filterData)
        {
            var stores = await _storeRepo.getStoresByAllFilters(filterData);
            return Ok(stores);
        }

        [HttpGet("{country}")]
        public async Task<IActionResult> GetStoresByCountry(string country)
        {
            var stores = await _storeRepo.getStoresByCountry(country);
            return Ok(stores);
        }
        [HttpGet("{StoreID}")]
        public async Task<IActionResult> AddStoreClickCount(int StoreID)
        {
            var res = await _storeRepo.clickStoreCount(StoreID);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> getTopWeeklyStores()
        {
            var res = await _countDetailsRepo.getWeeklyTopStores();
            return Ok(res);
        }
        // my code
        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetStoreById(int storeId)
        {
            var store = await _storeRepo.getStoreById(storeId);

            if (store == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "No such Store Exist",
                    Title = "",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(store);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetEmailByUserId(string userId)
        {
            var email = await _storeRepo.getEmailByUserId(userId);
            return Ok(email);
        }
        // my code end
    }
}