using DataAccess.Data;
using Models;
using Newtonsoft.Json;
using Sukhdari_Client.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Sukhdari_Client.Service
{
    public class UserIpService : IUserIpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public UserIpService(IHttpClientFactory httpClientFactory,HttpClient httpClient)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;

        }
        public async Task<UserIpDTO> GetUserIPAsync()
        {
            var client = _httpClientFactory.CreateClient("IP");
            return await client.GetFromJsonAsync<UserIpDTO>("/");
        }

        public async void StoreIp(UserIpDTO userIpDTO)
        {
            // var body=JsonConvert.SerializeObject(userIpDTO);
            // var content=new StringContent(body,System.Text.Encoding.UTF8,"application/json");
            var response=await _httpClient.PostAsJsonAsync<UserIpDTO>("api/UserIp/Create",userIpDTO);
            
        }
    }
}
