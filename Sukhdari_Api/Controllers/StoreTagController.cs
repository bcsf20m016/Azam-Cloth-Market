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
    public class StoreTagController : Controller
    {
        private readonly IStoreTagRepo _storeTagRepo;
        public StoreTagController(IStoreTagRepo storeTagRepo)
        {
            _storeTagRepo = storeTagRepo;
        }

        [HttpGet("{tagName}")]
        public async Task<IActionResult> GetStoresByTags(string tagName)
        {
            var stores = await _storeTagRepo.getStoresWithTags(tagName);
            return Ok(stores);
        }
        [HttpGet]
        public async Task<IActionResult> getAllStoreTags()
        {
            var stores = await _storeTagRepo.getAllStoreTags();
            return Ok(stores);
        }
    }
}