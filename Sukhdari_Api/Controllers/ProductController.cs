using Business.IRepo;
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
    public class ProductController : Controller
    {
        private readonly IStoreRepo _storeRepo;
        private readonly IProductRepo _productRepo;
        public ProductController(IProductRepo productRepo, IStoreRepo storeRepo)
        {
            _productRepo = productRepo;
            _storeRepo = storeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepo.getAllProducts();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetSpecificProduct(int? productId)
        {
            var products = await _productRepo.getAllProducts();
            var productToFind = products.FirstOrDefault(i => i.Id == productId);

            if (productId == null || productToFind == null)
            {
                return BadRequest(new ErrorModelDTO() { ErrorMessage = "Invalid Product Id", Title = "", StatusCode = StatusCodes.Status400BadRequest });

            }
            return Ok(productToFind);
        }

        [HttpGet("{productName}")]
        public async Task<IActionResult> GetStoresByProductName(string productName)
        {
            var stores = await _productRepo.getStoresByProductName(productName);
            return Ok(stores);
        }

        [HttpGet("{low}/{high}")]
        public async Task<IActionResult> getStoresByPriceRange(int low, int high)
        {
            var stores = await _productRepo.getStoresByProductPriceRange(low, high);
            return Ok(stores);
        }
        [HttpGet("{storeId}")]
        public async Task<IActionResult> getDiscountedProducts(int storeId)
        {
            var products = await _productRepo.getDiscountedProducts(storeId);
            return Ok(products);
        }

        [HttpGet("{storeId}/{productId}")]
        public async Task<IActionResult> getProduct(int storeId, int productId)
        {
            var product = await _productRepo.GetProduct(productId, storeId);
            return Ok(product);
        }
        [HttpGet("{productID}")]
        public async Task<IActionResult> AddProductClickCount(int productID)
        {
            var res = await _productRepo.clickProductCount(productID);
            return Ok();
        }

    }
}