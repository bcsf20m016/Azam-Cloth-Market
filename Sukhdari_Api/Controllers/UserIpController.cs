using Business.IRepo;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Api.Controllers
{
    [Route("api/[controller]/[action]")]

    public class UserIpController : Controller
    {
        private readonly IUserIPRepo _userIPRepo;
        public UserIpController(IUserIPRepo userIPRepo)
        {
            _userIPRepo = userIPRepo;
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] UserIpDTO dto)
        {
            var res=await _userIPRepo.createIp(dto);
            return Ok();
        }
    }
}
